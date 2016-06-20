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
		public string TenantId { get; set; }

		private readonly IStorageService storageService;

		public SettingsViewModel(IStorageService storageService)
		{
			this.storageService = storageService;
			LoadData();
		}

		public void SaveData()
		{
			storageService.Write(nameof(NotesToShowCount), NotesToShowCount);
			storageService.Write(nameof(SortAscending), SortAscending);
			storageService.Write(nameof(TenantId), TenantId);
		}
		private void LoadData()
		{
			NotesToShowCount = storageService.Read<int>(nameof(NotesToShowCount), 5);
			SortAscending = storageService.Read<bool>(nameof(SortAscending), false);
			TenantId = storageService.Read<string>(nameof(TenantId), "S1510237029");
		}
	}
}
