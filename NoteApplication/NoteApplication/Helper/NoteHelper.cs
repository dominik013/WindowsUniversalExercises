using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NoteApplication.Helper
{
	public class NoteHelper
	{
		public List<Note> NoteList { get; set; } = new List<Note>();
		public int NoteCount { get; set; } = 10;
		public bool SortAscending { get; set; } = false;

		public static NoteHelper Instance { get; } = new NoteHelper();

		public void AddNote(Note note)
		{
			NoteList.Add(note);
		}

		public void RemoveNote(Note note)
		{
			NoteList.Remove(note);
		}

		public ReadOnlyCollection<Note> GetNotes(int count)
		{
			if (SortAscending)
			{
				return NoteList.OrderBy(note => note.CreatedTime).Take(count).ToList().AsReadOnly();
			}
			return NoteList.OrderByDescending(note => note.CreatedTime).Take(count).ToList().AsReadOnly();
		}

		public ReadOnlyCollection<Note> GetNotesThatContain(string s, DateTime from, DateTime to)
		{
			List<Note> list = NoteList.OrderBy(note => note.CreatedTime).Where(note => note.Content.Contains(s)
				&& (DateTime.Compare(note.CreatedTime, from) >= 0)
				&& (DateTime.Compare(note.CreatedTime, to) <= 0)).ToList();

			if (SortAscending)
			{
				return list.AsReadOnly();
			}

			list.Reverse();
			return list.AsReadOnly();
		}
	}
}
