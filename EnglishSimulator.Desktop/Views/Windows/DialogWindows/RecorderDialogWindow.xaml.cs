namespace EnglishSimulator.Desktop.Views.Windows.DialogWindows
{
	/// <summary>
	/// Логика взаимодействия для RecorderDialogWindow.xaml
	/// </summary>
	public partial class RecorderDialogWindow : Window
	{
		public RecorderDialogWindow()
		{
			InitializeComponent();
		}

		public Task<RecorderDialogMessage?> ShowRecorderDialogWindowAsync()
		{
			var taskCompletionSource =
				new TaskCompletionSource<RecorderDialogMessage?>();

			RecorderDialogViewModel viewModel = (RecorderDialogViewModel)DataContext;
			if (viewModel is { })
			{
				viewModel.Closed += (success) =>
				{
					if (success)
					{
						taskCompletionSource.SetResult(new RecorderDialogMessage
						{
							AudioBuffer = viewModel.AudioBuffer,
							WaveFormat = viewModel.WaveFormat!
						});
					}
					else
					{
						taskCompletionSource.SetResult(null);
					}
					this.Close();
				};
			}

			this.ShowDialog();
			return taskCompletionSource.Task;
		}

		private void pnlControlBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				DragMove();
		}
    }
}
