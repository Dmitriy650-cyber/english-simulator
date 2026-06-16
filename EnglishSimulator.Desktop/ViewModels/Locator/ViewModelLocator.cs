namespace EnglishSimulator.Desktop.ViewModels.Locator
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
        public AddDeckViewModel AddDeckViewModel => App.Services.GetRequiredService<AddDeckViewModel>();
        public DialogViewModel DialogViewModel => App.Services.GetRequiredService<DialogViewModel>();
    }
}
