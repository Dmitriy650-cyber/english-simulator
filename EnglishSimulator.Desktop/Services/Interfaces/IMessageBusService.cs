namespace EnglishSimulator.Desktop.Services.Interfaces
{
	public interface IMessageBusService
	{
		public IDisposable RegisterHandler<T>(Action<T> handler);
		public void Send<T>(T message);
	}
}
