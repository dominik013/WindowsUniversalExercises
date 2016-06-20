using NoteApplication.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteApplication.Interfaces
{
	public interface IDataService
	{
		Task<IEnumerable<Note>> GetAllNotes();

		Task AddNote(Note note);

		Task SaveNote(Note note);

		Task DeleteNote(Note note);
	}
}
