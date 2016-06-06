using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Model
{
	public class Note
	{
		public Note(string content, DateTime createdTime)
		{
			Content = content;
			CreatedTime = createdTime;
		}
		public string Content { get; set; }
		public DateTime CreatedTime { get; set; }
	}
}
