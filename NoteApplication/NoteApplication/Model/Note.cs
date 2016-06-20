using Newtonsoft.Json;
using System;

namespace NoteApplication.Model
{
	public class Note
	{
		public Note(string title, string content, DateTime createdTime)
		{
			Title = title;
			Content = content;
			CreatedTime = createdTime;
		}
		public string Title { get; set; }
		public string Content { get; set; }
		public int Id { get; set; }
		[JsonProperty("CreatedAt")]
		public DateTime CreatedTime { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
