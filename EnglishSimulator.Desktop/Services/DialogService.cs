namespace EnglishSimulator.Desktop.Services
{
    public class DialogService : IDialogService, ISingletonDependency
    {
        public Task<string?> ShowAddDeckDialogAsync(string title)
        {
            var dialog = new InputDialogWindow
            {
                DataContext = new InputDialogViewModel(title)
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

        public Task<Sentence?> ShowSentenceDialogAsync(Sentence? sentence)
        {
            var dialog = new SentenceDialogWindow
            {
                DataContext = new SentenceDialogViewModel(sentence)
            };

            return dialog.ShowSentenceDialogWindowAsync();
        }
    }
}
