namespace EnglishSimulator.Desktop.ViewModels.Base
{
	public class ViewModel(IMessageBoxService messageBoxService) : ViewModelBase
	{
		protected IMessageBoxService MessageBoxService = messageBoxService;

		#region Свойства

		/// <summary>
		/// Заголовок страницы
		/// </summary>
		public string? Caption
		{
			get => field;
			set => Set(ref field, value);
		}

		/// <summary>
		/// Занята ли страница загрузкой данных?
		/// </summary>
		public bool IsBusy
		{
			get => field;
			set => Set(ref field, value);
		}

		/// <summary>
		/// Текст, отображаемый при загрузке данных
		/// </summary>
		public string? LoadingText
		{
			get => field;
			set => Set(ref field, value);
		}

		/// <summary>
		/// Загрузилась ли страница?
		/// </summary>
		public bool IsLoadedPage
		{
			get => field;
			set => Set(ref field, value);
		}

		#endregion

		public object? InputData;

		public virtual Task InitializeViewModelAsync() => Task.CompletedTask;

		protected async Task MakeRepositoryRequestAsync(Func<Task> request, string loadingText = "Loading...")
		{
			IsBusy = true;
			LoadingText = loadingText;

			try
			{
				await request.Invoke();
			}
			catch (Exception ex)
			{
				MessageBoxService.Error(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
