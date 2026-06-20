namespace EnglishSimulator.Desktop.Services.Interfaces
{
    public interface IDialogService
    {
		Task<string?> ShowAddDeckDialogAsync(string title);
		Task<bool> ShowDialogAsync(string question = "Are you sure?");
		Task<Sentence?> ShowSentenceDialogAsync(Sentence? sentence);
		Task<RecorderDialogMessage?> ShowRecoderDialogWindow();
		Task<List<RepetitionInterval>?> ShowRepetitionIntervalsDialogWindow(Deck deck);
	}
}
