using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NoteApplication.Helper;
using NoteApplication.Model;
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
		public string NumberOfShownItems { get; set; } = NoteHelper.Instance.NoteCount.ToString();

		private readonly NavigationService navigationService;

		public ReadNotesViewModel()
		{
			Notes = new ObservableCollection<Note>(NoteHelper.Instance.GetNotes(NoteHelper.Instance.NoteCount));
			navigationService = new NavigationService();
			navigationService.Configure("NoteDetailsPage", typeof(Pages.NoteDetails));
		}
	}
}
