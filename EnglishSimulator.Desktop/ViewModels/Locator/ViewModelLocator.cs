namespace EnglishSimulator.Desktop.ViewModels.Locator
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.Services.GetRequiredService<MainViewModel>();
        public InputDialogViewModel InputDialogViewModel => App.Services.GetRequiredService<InputDialogViewModel>();
        public DialogViewModel DialogViewModel => App.Services.GetRequiredService<DialogViewModel>();
    }
}
