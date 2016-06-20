using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Interfaces;
using NoteApplication.Model;
using System;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace NoteApplication.ViewModel
{
	// INPC injected by Fody
	public class NewNoteViewModel : ViewModelBase
	{
		public string NoteText { get; set; }
		public DateTime DateCreated { get; set; }
		private double Latitude { get; set; }
		private double Longitude { get; set; } 
		
		private DispatcherTimer dispatcherTimer;
		private readonly NavigationService navigationService;

		public RelayCommand AddNoteCommand { get; }
		public RelayCommand NavigateBackCommand { get; }

		public bool CanAddNote => !string.IsNullOrEmpty(NoteText);

		private IDataService dataservice;

		public NewNoteViewModel(IDataService dataservice)
		{
			this.dataservice = dataservice;

			AddNoteCommand = new RelayCommand(AddNote);
			NavigateBackCommand = new RelayCommand(NavigateBack);

			DateCreated = DateTime.Now;
			navigationService = new NavigationService();

			SetupDateCreated();
		}

		public async void NavigateBack()
		{
			if (CanAddNote)
			{
				var dialog = new MessageDialog("Do you really want to cancel?");
				var commandYes = new UICommand("Yes");
				var commandNo = new UICommand("No");
				dialog.Commands.Add(commandYes);
				dialog.Commands.Add(commandNo);
				var command = await dialog.ShowAsync();

				if (command == commandNo)
					return;
			}
			NoteText = "";
			navigationService.GoBack();
		}

		private void AddNote()
		{
			dataservice.AddNote(new Note(NoteText, DateCreated, Latitude, Longitude));
			NoteText = "";
			navigationService.GoBack();
		}

		private async void GetCurrentLocation()
		{
			var access = await Geolocator.RequestAccessAsync();

			switch (access)
			{
				case GeolocationAccessStatus.Allowed:

					var geolocator = new Geolocator();
					var geopositon = await geolocator.GetGeopositionAsync();
					var geopoint = geopositon.Coordinate.Point;

					Latitude = geopoint.Position.Latitude;
					Longitude = geopoint.Position.Longitude;
					break;
				case GeolocationAccessStatus.Unspecified:
				case GeolocationAccessStatus.Denied:
					break;
			}
		}

		/// <summary>
		/// Creates the clock which is showing the current time
		/// </summary>
		private void SetupDateCreated()
		{
			if (dispatcherTimer == null)
			{
				dispatcherTimer = new DispatcherTimer();
				dispatcherTimer.Tick += UpdateDateCreated;
				dispatcherTimer.Interval = new TimeSpan(0, 0, 1);   //Updates every second
				dispatcherTimer.Start();
			}
		}

		private void UpdateDateCreated(object sender, object e)
		{
			DateCreated = DateTime.Now;
		}
	}
}
