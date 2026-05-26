namespace EnglishSimulator.Desktop.Services.Interfaces
{
	internal interface IMessageBusService
	{
		public IDisposable RegisterHandler<T>(Action<T> handler);
		public void Send<T>(T message);
	}
}
