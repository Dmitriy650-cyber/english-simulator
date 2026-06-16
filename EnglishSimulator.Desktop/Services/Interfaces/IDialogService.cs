namespace EnglishSimulator.Desktop.Services.Interfaces
{
    public interface IDialogService
    {
		Task<string?> ShowAddDeckDialogAsync();
		Task<bool> ShowDialogAsync(string question = "Are you sure?");
	}
}
