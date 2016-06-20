
using Windows.Devices.Geolocation;

namespace NoteApplication.Model
{
	public class POI
	{
		public POI(Note note, Geopoint location)
		{
			this.Note = note;
			this.Location = location;
		}
		public Note Note { get; set; }
		public Geopoint Location { get; set; }
	}
}
