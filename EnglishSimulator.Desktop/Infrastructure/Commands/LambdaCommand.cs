namespace EnglishSimulator.Desktop.Infrastructure.Commands
{
	internal class LambdaCommand : Command
	{
		private readonly Action<object> _execute;
		private readonly Func<object, bool>? _canExecute;

		public LambdaCommand(Action execute, Func<bool>? canExecute = null) :
			this(n => execute(), canExecute is null ? (Func<object, bool>?)null : n => canExecute())
		{

		}

		public LambdaCommand(Action<object> execute, Func<object, bool>? canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		protected override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

		protected override void Execute(object parameter) => _execute(parameter);
	}
}
