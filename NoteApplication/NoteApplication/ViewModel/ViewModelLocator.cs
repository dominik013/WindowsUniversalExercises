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

		public NewNoteViewModel NewNoteViewModel => ServiceLocator.Current.GetInstance<NewNoteViewModel>();
		public OverviewViewModel OverviewViewModel => ServiceLocator.Current.GetInstance<OverviewViewModel>();
		public ReadNotesViewModel ReadNotesViewModel => ServiceLocator.Current.GetInstance<ReadNotesViewModel>();
		public SearchNotesViewModel SearchNotesViewModel => ServiceLocator.Current.GetInstance<SearchNotesViewModel>();
		public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
	}
}
