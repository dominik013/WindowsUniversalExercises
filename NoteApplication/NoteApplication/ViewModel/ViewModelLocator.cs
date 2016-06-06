using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace NoteApplication.ViewModel
{
	public class ViewModelLocator
	{
		static ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
			SimpleIoc.Default.Register<NewNoteViewModel>();
			SimpleIoc.Default.Register<OverviewViewModel>();
			SimpleIoc.Default.Register<ReadNotesViewModel>();
			SimpleIoc.Default.Register<SearchNotesViewModel>();
			SimpleIoc.Default.Register<SettingsViewModel>();
		}

		public NewNoteViewModel NewNoteViewModel => new NewNoteViewModel();
		public OverviewViewModel OverviewViewModel => new OverviewViewModel();
		public ReadNotesViewModel ReadNotesViewModel => new ReadNotesViewModel();
		public SearchNotesViewModel SearchNotesViewModel => new SearchNotesViewModel();
		public SettingsViewModel SettingsViewModel => new SettingsViewModel();
	}
}
