using GalaSoft.MvvmLight.Command;
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

		private bool sortAscending = NoteHelper.Instance.SortAscending;
		public bool SortAscending
		{
			get
			{
				return sortAscending;
			}
			set
			{
				sortAscending = value;
				NoteHelper.Instance.SortAscending = value;
			}
		}

		public RelayCommand SaveCommand { get; }
		public RelayCommand LoadCommand { get; }

		public SettingsViewModel()
		{
			SaveCommand = new RelayCommand(SaveData);
			LoadCommand = new RelayCommand(LoadData);
		}

		private void SaveData()
		{

		}
		private void LoadData()
		{

		}
	}
}
