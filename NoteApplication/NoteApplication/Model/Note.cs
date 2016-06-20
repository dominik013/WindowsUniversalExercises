using System;

namespace NoteApplication.Model
{
	public class Note
	{
		public Note(string content, DateTime createdTime, double latitude, double longitude)
		{
			Content = content;
			CreatedTime = createdTime;
			Latitude = latitude;
			Longitude = longitude;
		}
		public string Content { get; set; }
		public DateTime CreatedTime { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
