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
			SimpleIoc.Default.Register<NoteDetailsViewModel>();
		}

		public NewNoteViewModel NewNoteViewModel => ServiceLocator.Current.GetInstance<NewNoteViewModel>();
		public OverviewViewModel OverviewViewModel => ServiceLocator.Current.GetInstance<OverviewViewModel>();
		public ReadNotesViewModel ReadNotesViewModel => new ReadNotesViewModel();
		public SearchNotesViewModel SearchNotesViewModel => new SearchNotesViewModel();
		public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
		public NoteDetailsViewModel NoteDetailsViewModel => new NoteDetailsViewModel();
	}
}
