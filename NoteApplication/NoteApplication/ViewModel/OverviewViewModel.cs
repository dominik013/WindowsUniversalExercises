using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel
{
	public class OverviewViewModel : ViewModelBase
	{
		private readonly NavigationService navigationService;

		public RelayCommand NavigateNewNoteCommand { get; }
		public RelayCommand NavigateReadNotesCommand { get; }

		public OverviewViewModel()
		{
			navigationService = new NavigationService();
			navigationService.Configure("NewNotePage", typeof(Pages.NewNotePage));
			navigationService.Configure("ReadNotesPage", typeof(Pages.ReadNotes));

			NavigateNewNoteCommand = new RelayCommand(NavigateToNewNote);
			NavigateReadNotesCommand = new RelayCommand(NavigateToReadNotes);
		}
		public void NavigateToNewNote()
		{
			navigationService.NavigateTo("NewNotePage");
		}
		public void NavigateToReadNotes()
		{
			navigationService.NavigateTo("ReadNotesPage");
		}
		//public void NavigateBack()
		//{
		//	navigationService.GoBack();
		//}
	}
}
