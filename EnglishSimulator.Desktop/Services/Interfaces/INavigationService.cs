namespace EnglishSimulator.Desktop.Services.Interfaces
{
	public interface INavigationService
	{
		void NavigateTo(string pageName, object inputData);
		void NavigateTo<TViewModel>() where TViewModel : ViewModel;
	}
}
