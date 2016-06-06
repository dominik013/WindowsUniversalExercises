using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace NoteApplication.ViewModel
{
	// INPC injected by Fody
	public class NewNoteViewModel : ViewModelBase
	{
		public Note NoteToAdd { get; set; }
		public string NoteText { get; set; }
		public DateTime DateCreated { get; set; }
		public RelayCommand AddNoteCommand { get; }

		private DispatcherTimer dispatcherTimer;

		private readonly NavigationService navigationService;
		public RelayCommand NavigateBackCommand { get; }

		public bool CanAddNote => !string.IsNullOrEmpty(NoteText);	

		public NewNoteViewModel()
		{
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
			navigationService.GoBack();
		}

		private void AddNote()
		{
			NoteHelper.Instance.AddNote(new Note(NoteText, DateCreated));
			navigationService.GoBack();
		}

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
