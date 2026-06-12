namespace EnglishSimulator.Desktop.Views.Pages.Base
{
	public class BasePage : UserControl
	{
		private readonly IServiceProvider? _serviceProvider;
		private bool _isLoadedPage = false;

		protected ViewModel? _viewModel { get; set; }
		protected event EventHandler? OnPageInitialized;

		public static readonly DependencyProperty ViewModelTypeProperty =
			DependencyProperty.Register(
				nameof(ViewModelType),
				typeof(Type),
				typeof(BasePage),
				new PropertyMetadata(null));

		public Type? ViewModelType
		{
			get => (Type?)GetValue(ViewModelTypeProperty);
			set => SetValue(ViewModelTypeProperty, value);
		}

		protected BasePage()
		{
			if (Application.Current is App)
			{
				_serviceProvider = App.Services;
			}
		}

		protected override async void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			if (_serviceProvider != null && DataContext == null && ViewModelType != null && !_isLoadedPage)
			{
				DataContext = _serviceProvider.GetRequiredService(ViewModelType);
				_viewModel = (ViewModel)DataContext;

				OnPageInitialized?.Invoke(null, EventArgs.Empty);

				await _viewModel.InitializeViewModelAsync();

				_viewModel.IsLoadedPage = true;
				_isLoadedPage = true;
			}
		}
	}
}
