using Newtonsoft.Json;
using NoteApplication.Interfaces;
using NoteApplication.Model;
using NoteApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Helper
{
	public class CloudDataService : IDataService
	{
		private readonly SettingsViewModel settings;

		private string Uri => $"http://notesservice.azurewebsites.net/api/{settings.TenantId}/Notes";

		private readonly HttpClient client = new HttpClient();

		public CloudDataService(SettingsViewModel settings)
		{
			this.settings = settings;
		}

		public async Task<IEnumerable<Note>> GetAllNotes()
		{
			var json = await client.GetStringAsync(Uri);
			var notes = JsonConvert.DeserializeObject<IEnumerable<Note>>(json);
			return notes;
		}

		public async Task AddNote(Note note)
		{
			var json = JsonConvert.SerializeObject(note);
			var response = await client.PostAsync(Uri, new JsonContent(json));
		}

		public async Task SaveNote(Note note)
		{
			var json = JsonConvert.SerializeObject(note);
			await client.PutAsync($"{Uri}/{note.Id}", new JsonContent(json));
		}

		public async Task DeleteNote(Note note)
		{
			await client.DeleteAsync($"{Uri}/{note.Id}");
		}
	}

	public class JsonContent : StringContent
	{
		public JsonContent(string content) : this(content, Encoding.UTF8) { }

		private JsonContent(string content, Encoding encoding, string mediaType = "application/json") : base(content, encoding, mediaType) { }
	}
}
