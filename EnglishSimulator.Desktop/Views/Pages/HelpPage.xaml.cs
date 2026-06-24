using System.Diagnostics;

namespace EnglishSimulator.Desktop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для HelpPage.xaml
	/// </summary>
	public partial class HelpPage : BasePage
	{
		public HelpPage()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start(new ProcessStartInfo("https://github.com/Dmitriy650-cyber") { UseShellExecute = true });
		}
    }
}
