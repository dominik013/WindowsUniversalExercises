using NoteApplication.Model;
using NoteApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace NoteApplication.Pages
{
	/// <summary>
	/// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
	/// </summary>
	public sealed partial class MapviewPage : Page
	{
		public MapviewPage()
		{
			this.InitializeComponent();
		}

		public MapviewViewModel ViewModel => DataContext as MapviewViewModel;

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button button = e.OriginalSource as Button;
			if (button.DataContext is POI)
			{
				ViewModel.GoToNoteDetails(button.DataContext as POI);
			}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			ViewModel.GetNotes();
		}
	}
}
