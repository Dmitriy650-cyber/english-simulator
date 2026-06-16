namespace EnglishSimulator.Desktop.Services
{
    public class DialogService : IDialogService, ISingletonDependency
    {
        public Task<string?> ShowAddDeckDialogAsync()
        {
            var dialog = new AddDeckWindow();

            return dialog.ShowAddDeckWindowAsync();
        }
    }
}
