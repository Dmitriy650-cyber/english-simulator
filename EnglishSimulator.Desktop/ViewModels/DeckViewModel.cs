namespace EnglishSimulator.Desktop.ViewModels
{
	internal class DeckViewModel : ViewModel
	{
		public ObservableCollection<string> TestItems { get; set; } = [
			"item 1",
			"item 2",
			"item 3"
			];
	}
}
