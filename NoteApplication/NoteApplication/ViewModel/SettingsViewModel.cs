using NoteApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.ViewModel
{
	public class SettingsViewModel
	{
		private string notesToShowCount;
		public string NotesToShowCount
		{
			get
			{
				return notesToShowCount;
			}
			set
			{
				int number;
				notesToShowCount = value;
				if (int.TryParse(value, out number))
				{
					NoteHelper.Instance.NoteCount = number;
				}
			}
		}
	}
}
