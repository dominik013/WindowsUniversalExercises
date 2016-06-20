using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NoteApplication.Helper
{
	public static class NoteHelper
	{
		public static ReadOnlyCollection<Note> GetNotes(IEnumerable<Note> notes, int count, bool ascending)
		{
			if (ascending)
			{
				return notes.OrderBy(note => note.CreatedTime).Take(count).ToList().AsReadOnly();
			}
			return notes.OrderByDescending(note => note.CreatedTime).Take(count).ToList().AsReadOnly();
		}

		public static ReadOnlyCollection<Note> GetNotesThatContain(IEnumerable<Note> notes, string s, DateTime from, DateTime to, bool ascending)
		{
			List<Note> list = notes.OrderBy(note => note.CreatedTime).Where(note => note.Content.Contains(s)
				&& (DateTime.Compare(note.CreatedTime, from) >= 0)
				&& (DateTime.Compare(note.CreatedTime, to) <= 0)).ToList();

			if (ascending)
			{
				return list.AsReadOnly();
			}

			list.Reverse();
			return list.AsReadOnly();
		}
	}
}
