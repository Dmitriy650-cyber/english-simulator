namespace EnglishSimulator.Desktop.Views.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для SentenceDialogWindow.xaml
    /// </summary>
    public partial class SentenceDialogWindow : Window
    {
        public SentenceDialogWindow()
        {
            InitializeComponent();
        }

		public Task<Sentence?> ShowSentenceDialogWindowAsync()
		{
			var taskCompletionSource = 
				new TaskCompletionSource<Sentence?>();

			SentenceDialogViewModel viewModel = (SentenceDialogViewModel)DataContext;
			if (viewModel is { })
			{
				viewModel.Closed += (success) =>
				{
					if (success)
					{
						taskCompletionSource.SetResult(new Sentence
						{
							RussianText = viewModel.RussianText!,
							RussianAudio = viewModel.RussianAudio!,
							EnglishText = viewModel.EnglishText!,
							EnglishAudio = viewModel.EnglishAudio!
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
