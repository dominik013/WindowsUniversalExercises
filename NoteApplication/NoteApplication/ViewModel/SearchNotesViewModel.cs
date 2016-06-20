using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Interfaces;
using NoteApplication.Model;
using System;
using System.Collections.ObjectModel;

namespace NoteApplication.ViewModel
{
	public class SearchNotesViewModel : ViewModelBase
	{
		public ObservableCollection<Note> Notes { get; set; }
		public string SearchText { get; set; } = "";

		public DateTime From { get; set; } = new DateTime(2000, 1, 1);
		public DateTime To { get; set; } = DateTime.Now;

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

		private readonly NavigationService navigationService;
		private readonly IDataService dataservice;
		private readonly SettingsViewModel settings;

		public SearchNotesViewModel(IDataService dataservice, SettingsViewModel settings)
		{
			this.dataservice = dataservice;
			this.settings = settings;

			navigationService = new NavigationService();
			navigationService.Configure("NoteDetailsPage", typeof(Pages.NoteDetails));

			PropertyChanged += SearchNotesViewModel_PropertyChanged;
			To = To.AddDays(1);
			SearchNotes();
		}

		private void SearchNotesViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
		{
			if (args.PropertyName == nameof(SearchText)
				|| args.PropertyName == nameof(From)
				|| args.PropertyName == nameof(To)) {
				SearchNotes();
			}
		}

		private async void SearchNotes()
		{
			var notes = await dataservice.GetAllNotes();
			Notes = new ObservableCollection<Note>(NoteHelper.GetNotesThatContain(notes, SearchText, From, To, settings.SortAscending));
		}
	}
}
