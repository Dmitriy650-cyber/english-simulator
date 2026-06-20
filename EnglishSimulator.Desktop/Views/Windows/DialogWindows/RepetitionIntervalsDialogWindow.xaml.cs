namespace EnglishSimulator.Desktop.Views.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для RepetitionIntervalsDialogWindow.xaml
    /// </summary>
    public partial class RepetitionIntervalsDialogWindow : Window
    {
        public RepetitionIntervalsDialogWindow()
        {
            InitializeComponent();
        }

		public Task<List<RepetitionInterval>?> ShowRepetitionIntervalsDialogWindowAsync()
		{
			var taskCompletionSource =
				new TaskCompletionSource<List<RepetitionInterval>?>();

			RepetitionIntervalsDialogViewModel viewModel = (RepetitionIntervalsDialogViewModel)DataContext;
			if (viewModel is { })
			{
				viewModel.Closed += (success) =>
				{
					if (success)
					{
						taskCompletionSource.SetResult(viewModel.RepetitionIntervals);
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
