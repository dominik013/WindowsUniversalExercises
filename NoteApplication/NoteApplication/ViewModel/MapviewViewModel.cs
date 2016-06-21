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
		public string Token => "HCc9KftT2VAs63i057Uw~YmhZn11F3eY94bnV2S3LiA~Asr9u_NLHurZJl5glhrmUrubmlt5eRmJAHEBwMf5j3rItzxJMzBDOy4BKB9m-MnD";
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
			RaisePropertyChanged(nameof(POIToShow));
		}
	}
}
