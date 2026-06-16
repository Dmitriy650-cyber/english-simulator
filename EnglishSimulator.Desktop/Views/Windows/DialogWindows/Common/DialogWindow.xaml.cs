namespace EnglishSimulator.Desktop.Views.Windows.DialogWindows.Common
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

		public Task<bool> ShowDialogWindowAsync()
		{
			var taskCompletionSource = new TaskCompletionSource<bool>();

			DialogViewModel viewModel = (DialogViewModel)DataContext;
			if (viewModel is { })
			{
				viewModel.Closed += (success) =>
				{
					if (success)
					{
						taskCompletionSource.SetResult(true);
					}
					else
					{
						taskCompletionSource.SetResult(false);
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
