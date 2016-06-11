using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Model;
using System;
using System.Collections.ObjectModel;

namespace NoteApplication.ViewModel
{
	public class SearchNotesViewModel : ViewModelBase
	{
		public ObservableCollection<Note> Notes { get; set; }
		public string SearchText { get; set; } = "";

		public RelayCommand SearchCommand { get; }

		public DateTime From { get; set; } = new DateTime(2000, 1, 1);
		public DateTime To { get; set; } = DateTime.Now;

		private Note selectedItem;
		public Note SelectedItem
		{
			get
			{
				return selectedItem;
			}
			set
			{
				if (value == selectedItem)
					return;

				navigationService.NavigateTo("NoteDetailsPage");
				Mediator.Instance.SendMessage("SelectedNote", value);
			}
		}

		private readonly NavigationService navigationService;

		public SearchNotesViewModel()
		{
			SearchCommand = new RelayCommand(SearchNotes);
			navigationService = new NavigationService();
			navigationService.Configure("NoteDetailsPage", typeof(Pages.NoteDetails));
		}

		private void SearchNotes()
		{
			Notes = new ObservableCollection<Note>(NoteHelper.Instance.GetNotesThatContain(SearchText, From, To));
		}
	}
}
