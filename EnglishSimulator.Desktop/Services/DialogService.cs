namespace EnglishSimulator.Desktop.Services
{
    public class DialogService : IDialogService, ISingletonDependency
    {
        public Task<string?> ShowAddDeckDialogAsync()
        {
            var dialog = new AddDeckWindow
            {
                DataContext = new AddDeckViewModel()
            };

            return dialog.ShowAddDeckWindowAsync();
        }

        public Task<bool> ShowDialogAsync(string question = "Are you sure?")
        {
            var dialog = new DialogWindow
            {
                DataContext = new DialogViewModel(question)
            };

            return dialog.ShowDialogWindowAsync();
        }
    }
}
