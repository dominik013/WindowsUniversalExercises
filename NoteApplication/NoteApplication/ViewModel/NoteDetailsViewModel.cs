using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Interfaces;
using NoteApplication.Model;
using System;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.UI.Xaml.Controls.Maps;

namespace NoteApplication.ViewModel
{
	public class NoteDetailsViewModel : ViewModelBase
	{
		public Note Note { get; set; }
		public string NoteTitle { get; set; }
		public string NoteText { get; set; }

		public RelayCommand CancelCommand { get; }
		public RelayCommand SaveCommand { get; }
		public RelayCommand DeleteCommand { get; }

		private NavigationService navigationService;
		private readonly IDataService dataservice;
		private readonly ReadNotesViewModel readNotesViewModel;
		private readonly MapviewViewModel mapviewViewModel;

		// Map related
		public MapStyle MapStyle => (MapStyle)Enum.Parse(typeof(MapStyle), MapStyleName);
		public string MapStyleName { get; set; } = "Road";
		public string Token => "HCc9KftT2VAs63i057Uw~YmhZn11F3eY94bnV2S3LiA~Asr9u_NLHurZJl5glhrmUrubmlt5eRmJAHEBwMf5j3rItzxJMzBDOy4BKB9m-MnD";

		public double Zoom { get; set; } = 5;
		public Geopoint LocationTaken { get; set; } 

		public NoteDetailsViewModel(IDataService dataservice, ReadNotesViewModel readNotesViewModel, MapviewViewModel mapviewViewModel)
		{
			this.dataservice = dataservice;
			this.readNotesViewModel = readNotesViewModel;
			this.mapviewViewModel = mapviewViewModel;

			CancelCommand = new RelayCommand(CancelEdit);
			SaveCommand = new RelayCommand(SaveNote);
			DeleteCommand = new RelayCommand(DeleteNote);

			Mediator.Instance.Register("SelectedNote", SetNote);
			navigationService = new NavigationService();
		}

		private void SetNote(object arg)
		{
			if (arg is Note)
			{
				Note = arg as Note;
				NoteText = Note.Content;
				NoteTitle = Note.Title;
				LocationTaken = new Geopoint(new BasicGeoposition() { Latitude = Note.Latitude, Longitude = Note.Longitude });
			}
		}

		private void CancelEdit()
		{
			navigationService.GoBack();
		}

		private void SaveNote()
		{
			Note.Content = NoteText;
			Note.Title = NoteTitle;
			dataservice.SaveNote(Note);
			readNotesViewModel.LoadNotes();
			navigationService.GoBack();
		}

		private void DeleteNote()
		{
			dataservice.DeleteNote(Note);
			readNotesViewModel.LoadNotes();
			navigationService.GoBack();
		}
	}
}
