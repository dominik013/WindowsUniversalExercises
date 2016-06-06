using NoteApplication.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NoteApplication.Helper
{
	public class NoteHelper
	{
		private List<Note> noteList = new List<Note>();
		public int NoteCount { get; set; } = 10;

		public static NoteHelper Instance { get; } = new NoteHelper();

		public void AddNote(Note note)
		{
			noteList.Add(note);
		}

		public ReadOnlyCollection<Note> GetNotes(int count)
		{
			return noteList.OrderByDescending(note => note.CreatedTime).Take(count).ToList().AsReadOnly();
		}

		public ReadOnlyCollection<Note> GetNotesThatContain(string s)
		{
			return noteList.OrderByDescending(note => note.CreatedTime).Where(note => note.Content.Contains(s)).ToList().AsReadOnly();
		}
	}
}
