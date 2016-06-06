using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NoteApplication.Helper;
using NoteApplication.Model;
using System;
using System.Collections.ObjectModel;

namespace NoteApplication.ViewModel
{
	public class SearchNotesViewModel : ViewModelBase
	{
		public ObservableCollection<Note> Notes { get; set; }
		public string SearchText { get; set; }
		public RelayCommand SearchCommand { get; }

		public DateTime From { get; set; } = new DateTime(2000, 1, 1);
		public DateTime To { get; set; } = DateTime.Now;
	

		public SearchNotesViewModel()
		{
			SearchCommand = new RelayCommand(SearchNotes);
			Notes = new ObservableCollection<Note>();
		}

		private void SearchNotes()
		{
			if (SearchText != null && SearchText.Length != 0)
			{
				Notes.Clear();
				var noteList = NoteHelper.Instance.GetNotesThatContain(SearchText);

				foreach (var note in noteList)
				{
					if ((DateTime.Compare(note.CreatedTime, From) >= 0) &&	// t1 is later than t2
						(DateTime.Compare(note.CreatedTime, To) <= 0))		// t1 is earlier than t2
					{
						Notes.Add(note);
					}
				}
			}
		}
	}
}
