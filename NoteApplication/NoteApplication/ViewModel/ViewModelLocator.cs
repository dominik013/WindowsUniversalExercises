using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using NoteApplication.Helper;
using NoteApplication.Interfaces;

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

			SimpleIoc.Default.Register<IStorageService, StorageService>();
			SimpleIoc.Default.Register<IDataService, OneTimeDataService>();
		}

		public NewNoteViewModel NewNoteViewModel => ServiceLocator.Current.GetInstance<NewNoteViewModel>();
		public OverviewViewModel OverviewViewModel => ServiceLocator.Current.GetInstance<OverviewViewModel>();
		public ReadNotesViewModel ReadNotesViewModel => ServiceLocator.Current.GetInstance<ReadNotesViewModel>();
		public SearchNotesViewModel SearchNotesViewModel => ServiceLocator.Current.GetInstance<SearchNotesViewModel>();
		public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
		public NoteDetailsViewModel NoteDetailsViewModel => ServiceLocator.Current.GetInstance<NoteDetailsViewModel>();
	}
}
