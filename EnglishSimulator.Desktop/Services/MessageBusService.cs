namespace EnglishSimulator.Desktop.Services
{
	public class MessageBusService : IMessageBusService, ISingletonDependency
	{
		private class Subscription<T> : IDisposable
		{
			private readonly WeakReference<MessageBusService> _bus;
			public Action<T> Handler { get; }
			public Subscription(MessageBusService bus, Action<T> handler)
			{
				Handler = handler;
				_bus = new(bus);
			}

			public void Dispose()
			{
				if (!_bus.TryGetTarget(out var bus)) return;

				var @lock = bus._lock;
				@lock.EnterWriteLock();
				var message_type = typeof(T);
				try
				{
					if (!bus._subscriptions.TryGetValue(message_type, out var refs)) return;

					var update_refs = refs.Where(n => n.IsAlive).ToList();

					WeakReference? current_ref = null;
					foreach (var @ref in update_refs)
						if (ReferenceEquals(@ref.Target, this))
						{
							current_ref = @ref;
							break;
						}

					if (current_ref is null) return;

					update_refs.Remove(current_ref);

					bus._subscriptions[message_type] = update_refs;
				}
				finally
				{
					@lock.ExitWriteLock();
				}
			}
		}

		private readonly Dictionary<Type, IEnumerable<WeakReference>> _subscriptions = new();
		private readonly ReaderWriterLockSlim _lock = new();

		public IDisposable RegisterHandler<T>(Action<T> handler)
		{
			var subscription = new Subscription<T>(this, handler);

			_lock.EnterWriteLock();
			try
			{
				var subscription_ref = new WeakReference(subscription);
				var message_type = typeof(T);
				_subscriptions[message_type] = _subscriptions.TryGetValue(
					message_type, out var subscriptions)
					? subscriptions.Append(subscription_ref)
					: new[] { subscription_ref };
			}
			finally
			{
				_lock.ExitWriteLock();
			}

			return subscription;
		}

		public void Send<T>(T message)
		{
			if (GetHandlers<T>() is not { } handlers) return;

			foreach (var handler in handlers)
			{
				handler(message);
			}
		}

		private IEnumerable<Action<T>>? GetHandlers<T>()
		{
			var handlers = new List<Action<T>>();
			var message_type = typeof(T);
			var exist_die_refs = false;

			_lock.EnterWriteLock();
			try
			{
				if (!_subscriptions.TryGetValue(message_type, out var refs)) return null;

				foreach (var @ref in refs)
					if (@ref.Target is Subscription<T> { Handler: var handler })
						handlers.Add(handler);
					else
						exist_die_refs = true;
			}
			finally
			{
				_lock.ExitWriteLock();
			}

			if (!exist_die_refs) return handlers;

			_lock.EnterWriteLock();
			try
			{
				if (_subscriptions.TryGetValue(message_type, out var refs))
					if (refs.Where(n => n.IsAlive).ToArray() is { Length: > 0 } new_refs)
						_subscriptions[message_type] = new_refs;
					else
						_subscriptions.Remove(message_type);
			}
			finally
			{
				_lock.ExitWriteLock();
			}

			return handlers;
		}
	}
}
