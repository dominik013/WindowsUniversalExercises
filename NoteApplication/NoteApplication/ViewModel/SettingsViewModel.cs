using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NoteApplication.Helper;
using NoteApplication.Interfaces;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.ViewModel
{
	public class SettingsViewModel : ViewModelBase
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

		private readonly IStorageService storageService;

		public SettingsViewModel(IStorageService storageService)
		{
			this.storageService = storageService;
			SaveCommand = new RelayCommand(SaveData);
			LoadCommand = new RelayCommand(LoadData);
		}

		private void SaveData()
		{
			storageService.Write(nameof(NotesToShowCount), NotesToShowCount);
			storageService.Write(nameof(SortAscending), SortAscending);
			storageService.Write(nameof(NoteHelper.Instance.NoteList), NoteHelper.Instance.NoteList);
		}
		private void LoadData()
		{
			NotesToShowCount = storageService.Read<string>(nameof(NotesToShowCount), "5");
			SortAscending = storageService.Read<bool>(nameof(SortAscending), false);
			NoteHelper.Instance.NoteList = storageService.Read<List<Note>>(nameof(NoteHelper.Instance.NoteList), new List<Note>());
		}
	}
}
