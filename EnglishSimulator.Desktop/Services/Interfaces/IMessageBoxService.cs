namespace EnglishSimulator.Desktop.Services.Interfaces
{
	public interface IMessageBoxService
	{
		public bool OpenFile(string title, out string? selectedFile, string filter = "Все файлы (*.*)|*.*");
		public bool OpenFiles(string title, out IEnumerable<string>? selectedFiles, string filter = "Все файлы (*.*)|*.*");
		public bool SaveFile(string title, out string? selectedFile, string? defaultFileName = null, string filter = "Все файлы (*.*)|*.*");
		public MessageBoxResult Dialog(string title, string message);
		public void Information(string message, string title = "Info");
		public void Warning(string message, string title = "Warning");
		public void Error(string message, string title = "Error");
	}
}
