using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Interfaces;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace NoteApplication.ViewModel
{
	public class MapviewViewModel : ViewModelBase
	{
		public MapStyle MapStyle => (MapStyle)Enum.Parse(typeof(MapStyle), MapStyleName);
		public string MapStyleName { get; set; } = "Road";
		public string Token => "ABqAfIfL8B3HIvAVMVmK~MSfiZYN7DoiZ0MlGVtjOHw~Aq0tWh2Z0wfet16va06U21q4LNDSvC4mjQSLJjhtL5MV8RMTKvWwoqDNGinI4dGn";
		public double Zoom { get; set; } = 2;
		public Geopoint Center { get; set; }

		public ObservableCollection<POI> POIToShow { get; set; }

		private readonly IDataService dataservice;
		private readonly NavigationService navigationService;

		public MapviewViewModel(IDataService dataservice)
		{
			navigationService = new NavigationService();
			navigationService.Configure("NoteDetailsPage", typeof(Pages.NoteDetails));
			this.dataservice = dataservice;
			POIToShow = new ObservableCollection<POI>();
			GetNotes();
		}

		public void GoToNoteDetails(POI poi)
		{
			navigationService.NavigateTo("NoteDetailsPage");
			Mediator.Instance.SendMessage("SelectedNote", poi.Note);
		}

		public async void GetNotes()
		{
			if (POIToShow != null)
			{
				POIToShow.Clear();
			}

			var notes = await dataservice.GetAllNotes();
			
			foreach (Note note in notes)
			{
				POIToShow.Add(new POI(note, new Geopoint(new BasicGeoposition() { Latitude = note.Latitude, Longitude = note.Longitude })));
			}
		}
	}
}
