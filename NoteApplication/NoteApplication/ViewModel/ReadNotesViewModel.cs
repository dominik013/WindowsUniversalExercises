using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Interfaces;
using NoteApplication.Model;
using System.Collections;
using System.Collections.ObjectModel;

namespace NoteApplication.ViewModel
{
	public class ReadNotesViewModel : ViewModelBase
	{
		public ObservableCollection<Note> Notes { get; set; }
		private Note selectedItem;
		public Note SelectedItem
		{
			get
			{
				return selectedItem;
			}
			set
			{
				if (value == selectedItem)
					return;

				navigationService.NavigateTo("NoteDetailsPage");
				Mediator.Instance.SendMessage("SelectedNote", value);
			}
		}
		public int NumberOfShownItems { get; set; }

		private readonly NavigationService navigationService;
		private readonly SettingsViewModel settings;
		private readonly IDataService dataservice;

		public ReadNotesViewModel(SettingsViewModel settings, IDataService dataservice)
		{
			this.settings = settings;
			this.dataservice = dataservice;

			navigationService = new NavigationService();
			navigationService.Configure("NoteDetailsPage", typeof(Pages.NoteDetails));		
		}

		public async void LoadNotes()
		{
			NumberOfShownItems = settings.NotesToShowCount;
			var notes = await dataservice.GetAllNotes();
			notes = NoteHelper.GetNotes(notes, settings.NotesToShowCount, settings.SortAscending);
			Notes = new ObservableCollection<Note>(notes);
			RaisePropertyChanged(nameof(Notes));
		}
	}
}
