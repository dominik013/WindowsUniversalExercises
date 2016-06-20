using NoteApplication.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace NoteApplication.Pages
{
	/// <summary>
	/// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
	/// </summary>
	public sealed partial class Settings : Page
	{
		public Settings()
		{
			this.InitializeComponent();
		}

		public SettingsViewModel ViewModel => DataContext as SettingsViewModel;

		protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
			ViewModel.SaveData();
			base.OnNavigatingFrom(e);
		}
	}
}
