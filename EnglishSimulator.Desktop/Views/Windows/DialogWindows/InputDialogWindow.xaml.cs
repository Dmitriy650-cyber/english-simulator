namespace EnglishSimulator.Desktop.Views.Windows.DialogWindows
{
	/// <summary>
	/// Логика взаимодействия для InputDialogWindow.xaml
	/// </summary>
	public partial class InputDialogWindow : Window
    {
        public InputDialogWindow()
        {
            InitializeComponent();
        }

        public Task<string?> ShowAddDeckWindowAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<string?>();

			InputDialogViewModel viewModel = (InputDialogViewModel)DataContext;
            if (viewModel is { })
            {
                viewModel.Closed += (success) =>
                {
                    if (success)
                    {
                        taskCompletionSource.SetResult(viewModel.InputText);
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
