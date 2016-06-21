using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace NoteApplication.ViewModel
{
	public class OverviewViewModel : ViewModelBase
	{
		private readonly NavigationService navigationService;

		public RelayCommand NavigateNewNoteCommand { get; }
		public RelayCommand NavigateReadNotesCommand { get; }
		public RelayCommand NavigateSearchNotesCommand { get; }
		public RelayCommand NavigateMapviewCommand { get; }
		public RelayCommand NavigateSettingsCommand { get; }

		public OverviewViewModel()
		{
			navigationService = new NavigationService();
			navigationService.Configure("NewNotePage", typeof(Pages.NewNotePage));
			navigationService.Configure("ReadNotesPage", typeof(Pages.ReadNotes));
			navigationService.Configure("SearchNotesPage", typeof(Pages.SearchNotes));
			navigationService.Configure("MapviewPage", typeof(Pages.MapviewPage));
			navigationService.Configure("SettingsPage", typeof(Pages.Settings));

			NavigateNewNoteCommand = new RelayCommand(NavigateToNewNote);
			NavigateReadNotesCommand = new RelayCommand(NavigateToReadNotes);
			NavigateSearchNotesCommand = new RelayCommand(NavigateToSearchNotes);
			NavigateMapviewCommand = new RelayCommand(NavigateToMapview);
			NavigateSettingsCommand = new RelayCommand(NavigateToSettings);
		}
		public void NavigateToNewNote()
		{
			navigationService.NavigateTo("NewNotePage");
		}
		public void NavigateToReadNotes()
		{
			navigationService.NavigateTo("ReadNotesPage");
		}
		public void NavigateToSearchNotes()
		{
			navigationService.NavigateTo("SearchNotesPage");
		}
		public void NavigateToMapview()
		{
			navigationService.NavigateTo("MapviewPage");
		}
		public void NavigateToSettings()
		{
			navigationService.NavigateTo("SettingsPage");
		}
	}
}
