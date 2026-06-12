namespace EnglishSimulator.Desktop.Services.Interfaces
{
	public interface INavigationService
	{
		void NavigateTo(string pageName);
		void NavigateTo<TViewModel>() where TViewModel : ViewModel;
	}
}
