using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Interfaces;
using NoteApplication.Model;

namespace NoteApplication.ViewModel
{
	public class NoteDetailsViewModel : ViewModelBase
	{
		public Note Note { get; set; }
		public string NoteText { get; set; }

		public RelayCommand CancelCommand { get; }
		public RelayCommand SaveCommand { get; }
		public RelayCommand DeleteCommand { get; }

		private NavigationService navigationService;
		private readonly IDataService dataservice;
		private readonly ReadNotesViewModel readNotesViewModel;

		public NoteDetailsViewModel(IDataService dataservice, ReadNotesViewModel readNotesViewModel)
		{
			this.dataservice = dataservice;
			this.readNotesViewModel = readNotesViewModel;

			CancelCommand = new RelayCommand(CancelEdit);
			SaveCommand = new RelayCommand(SaveNote);
			DeleteCommand = new RelayCommand(DeleteNote);

			Mediator.Instance.Register("SelectedNote", SetNote);
			navigationService = new NavigationService();
		}

		private void SetNote(object arg)
		{
			if (arg is Note)
			{
				Note = arg as Note;
				NoteText = Note.Content;
			}
		}

		private void CancelEdit()
		{
			navigationService.GoBack();
		}

		private void SaveNote()
		{
			Note.Content = NoteText;
			readNotesViewModel.LoadNotes();
			navigationService.GoBack();
		}

		private void DeleteNote()
		{
			dataservice.DeleteNote(Note);
			readNotesViewModel.LoadNotes();
			navigationService.GoBack();
		}
	}
}
