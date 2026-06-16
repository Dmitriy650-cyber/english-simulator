namespace EnglishSimulator.Desktop.Views.Windows.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для AddDeckWindow.xaml
    /// </summary>
    public partial class AddDeckWindow : Window
    {
        public AddDeckWindow()
        {
            InitializeComponent();
        }

        public Task<string?> ShowAddDeckWindowAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<string?>();

            AddDeckViewModel viewModel = (AddDeckViewModel)DataContext;
            if (viewModel is { })
            {
                viewModel.Closed += (success) =>
                {
                    if (success)
                    {
                        taskCompletionSource.SetResult(viewModel.DeckName);
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
