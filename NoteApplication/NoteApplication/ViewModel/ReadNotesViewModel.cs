﻿using GalaSoft.MvvmLight;
using NoteApplication.Helper;
using NoteApplication.Model;
using System.Collections.ObjectModel;

namespace NoteApplication.ViewModel
{
	public class ReadNotesViewModel : ViewModelBase
	{
		public ObservableCollection<Note> Notes { get; }
		public string NumberOfShownItems { get; } = NoteHelper.Instance.NoteCount.ToString();

		public ReadNotesViewModel()
		{
			Notes = new ObservableCollection<Note>(NoteHelper.Instance.GetNotes(NoteHelper.Instance.NoteCount));
		}
	}
}
