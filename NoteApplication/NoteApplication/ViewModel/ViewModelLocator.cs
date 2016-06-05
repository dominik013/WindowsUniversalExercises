using GalaSoft.MvvmLight.Ioc;

namespace NoteApplication.ViewModel
{
	public class ViewModelLocator
	{
		static ViewModelLocator()
		{
			SimpleIoc.Default.Register<NewNoteViewModel>();
			SimpleIoc.Default.Register<OverviewViewModel>();
			SimpleIoc.Default.Register<ReadNotesViewModel>();
		}

		public NewNoteViewModel NewNoteViewModel => new NewNoteViewModel();
		public OverviewViewModel OverviewViewModel => new OverviewViewModel();
		public ReadNotesViewModel ReadNotesViewModel => new ReadNotesViewModel();
	}
}
