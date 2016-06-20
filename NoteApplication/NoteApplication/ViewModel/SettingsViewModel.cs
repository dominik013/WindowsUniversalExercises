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
		public int NotesToShowCount { get; set; } = 5;
		public bool SortAscending { get; set; }

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
		}
		private void LoadData()
		{
			NotesToShowCount = storageService.Read<int>(nameof(NotesToShowCount), 5);
			SortAscending = storageService.Read<bool>(nameof(SortAscending), false);
		}
	}
}
