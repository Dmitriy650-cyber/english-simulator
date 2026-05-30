using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace EnglishSimulator.Desktop.Views.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int sParam, int lParam);

		private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
		{
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

		private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

		private void btnClose_Click(object sender, RoutedEventArgs e)
		{
            Application.Current.Shutdown();
        }

		private void btnMaximize_Click(object sender, RoutedEventArgs e)
		{
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

		private void btnMinimize_Click(object sender, RoutedEventArgs e)
		{
            this.WindowState = WindowState.Minimized;
        }
    }
}
