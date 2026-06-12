namespace EnglishSimulator.Desktop.Services
{
	public class MessageBoxService : IMessageBoxService, ISingletonDependency
	{
		public bool OpenFile(string title, out string? selectedFile, string filter = "Все файлы (*.*)|*.*")
		{
			var file_dialog = new OpenFileDialog
			{
				Title = title,
				Filter = filter
			};

			if (file_dialog.ShowDialog() != true)
			{
				selectedFile = null;
				return false;
			}

			selectedFile = file_dialog.FileName;

			return true;
		}

		public bool OpenFiles(string title, out IEnumerable<string>? selectedFiles, string filter = "Все файлы (*.*)|*.*")
		{
			var file_dialog = new OpenFileDialog
			{
				Title = title,
				Filter = filter
			};

			if (file_dialog.ShowDialog() != true)
			{
				selectedFiles = Enumerable.Empty<string>();
				return false;
			}

			selectedFiles = file_dialog.FileNames;

			return true;
		}

		public bool SaveFile(string title, out string? selectedFile, string? defaultFileName = null, string filter = "Все файлы (*.*)|*.*")
		{
			var file_dialog = new SaveFileDialog
			{
				Title = title,
				Filter = filter
			};

			if (!string.IsNullOrWhiteSpace(defaultFileName))
				file_dialog.FileName = defaultFileName;

			if (file_dialog.ShowDialog() != true)
			{
				selectedFile = null;
				return false;
			}

			selectedFile = file_dialog.FileName;

			return true;
		}

		public MessageBoxResult Dialog(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);

		public void Information(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

		public void Warning(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);

		public void Error(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
	}
}
