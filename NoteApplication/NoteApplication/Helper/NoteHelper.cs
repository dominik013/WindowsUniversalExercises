using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NoteApplication.Helper
{
	public class NoteHelper
	{
		private List<Note> noteList = new List<Note>();
		public int NoteCount { get; set; } = 10;
		public bool SortAscending { get; set; } = false;

		public static NoteHelper Instance { get; } = new NoteHelper();

		public void AddNote(Note note)
		{
			noteList.Add(note);
		}

		public void RemoveNote(Note note)
		{
			noteList.Remove(note);
		}

		public ReadOnlyCollection<Note> GetNotes(int count)
		{
			if (SortAscending)
			{
				return noteList.OrderBy(note => note.CreatedTime).Take(count).ToList().AsReadOnly();
			}
			return noteList.OrderByDescending(note => note.CreatedTime).Take(count).ToList().AsReadOnly();
		}

		public ReadOnlyCollection<Note> GetNotesThatContain(string s)
		{
			if (SortAscending)
			{
				return noteList.OrderBy(note => note.CreatedTime).Where(note => note.Content.Contains(s)).ToList().AsReadOnly();
			}
			return noteList.OrderByDescending(note => note.CreatedTime).Where(note => note.Content.Contains(s)).ToList().AsReadOnly();
		}
	}
}
