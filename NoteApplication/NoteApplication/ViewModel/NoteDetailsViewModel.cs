using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public NoteDetailsViewModel()
		{
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
			navigationService.GoBack();
		}

		private void DeleteNote()
		{
			NoteHelper.Instance.RemoveNote(Note);
			navigationService.GoBack();
		}
	}
}
