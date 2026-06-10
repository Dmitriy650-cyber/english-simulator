namespace EnglishSimulator.Desktop.Services.Interfaces
{
	internal interface INavigationService
	{
		void NavigateTo(string pageName);
		void NavigateTo<TViewModel>() where TViewModel : ViewModel;
	}
}
