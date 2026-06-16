namespace EnglishSimulator.Desktop.ViewModels.DialogViewModels.Base
{
    public class DialogViewModelBase : ViewModelBase
    {
		private bool _isClosed;
		public event Action<bool>? Closed;

		#region Команды

		/// <summary>
		/// Подтвердить действия и закрыть окно
		/// </summary>
		public ICommand? OkCommand => new LambdaCommand(() => CloseDialog(true));

		/// <summary>
		/// Отменить действия и закрыть окно
		/// </summary>
		public ICommand CancelCommand => new LambdaCommand(() => CloseDialog(false));

		#endregion

		private void CloseDialog(bool result)
		{
			if (!_isClosed)
			{
				_isClosed = true;
				Closed?.Invoke(result);
			}
		}
	}
}
