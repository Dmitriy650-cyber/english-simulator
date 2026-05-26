namespace EnglishSimulator.Desktop.Services.Interfaces
{
	internal interface IMessageBoxService
	{
		public bool OpenFile(string title, out string? selectedFile, string filter = "Все файлы (*.*)|*.*");
		public bool OpenFiles(string title, out IEnumerable<string>? selectedFiles, string filter = "Все файлы (*.*)|*.*");
		public bool SaveFile(string title, out string? selectedFile, string? defaultFileName = null, string filter = "Все файлы (*.*)|*.*");
		public MessageBoxResult Dialog(string title, string message);
		public void Information(string title, string message);
		public void Warning(string title, string message);
		public void Error(string title, string message);
	}
}
