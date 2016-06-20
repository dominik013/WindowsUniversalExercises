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
		public string Token => "ABqAfIfL8B3HIvAVMVmK~MSfiZYN7DoiZ0MlGVtjOHw~Aq0tWh2Z0wfet16va06U21q4LNDSvC4mjQSLJjhtL5MV8RMTKvWwoqDNGinI4dGn";
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
