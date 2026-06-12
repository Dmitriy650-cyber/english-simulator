namespace EnglishSimulator.Desktop.ViewModels.Locator
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
    }
}
