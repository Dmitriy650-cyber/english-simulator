namespace EnglishSimulator.Desktop.ViewModels.Locator
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
    }
}
