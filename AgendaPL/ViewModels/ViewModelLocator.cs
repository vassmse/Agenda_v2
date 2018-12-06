using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AgendaPL.ViewModels
{
    // ViewModelLocator class because one ViewModel for all the Views -> DI
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}
