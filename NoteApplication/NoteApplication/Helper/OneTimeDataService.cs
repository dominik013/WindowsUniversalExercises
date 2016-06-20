using NoteApplication.Interfaces;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteApplication.Helper
{
	public class OneTimeDataService : IDataService
	{
		private readonly List<Note> notes;

		public OneTimeDataService()
		{
			notes = new List<Note>
						   {
							   new Note("test", DateTime.Now, 1, 2),
							   new Note("test2", DateTime.Now, 1, 2),
							   new Note("test3", DateTime.Now, 1, 2)
						   };
		}
		public async Task<IEnumerable<Note>> GetAllNotes()
		{
			return notes.AsReadOnly();
		}

		public async Task AddNote(Note note)
		{
			notes.Add(note);
		}

		public async Task SaveNote(Note note)
		{
			note.CreatedTime = DateTime.Now;
		}

		public async Task DeleteNote(Note note)
		{
			notes.Remove(note);
		}
	}
}