using AgendaPL.ViewModels;
using AgendaPL.Views.UserControl;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public LoginPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if ((bool)(e.Parameter))
                {
                    ViewModel.ErrorMessage = String.Empty;
                }
                else
                {
                    ViewModel.ErrorMessage = String.Empty;
                    ViewModel.OkMessage = String.Empty;
                }
            }
            catch
            {
                ViewModel.ErrorMessage = String.Empty;
                ViewModel.OkMessage = String.Empty;
            }


        }

        // User pressed sign in button
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 0);

            if (ViewModel.LoginButtonAction())
                Frame.Navigate(typeof(MainPage));

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }

        // Navigate to register page
        private void RegisterButtonTextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegisterPage));
        }

        private void RegisterButtonTextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        }

        private void RegisterButtonTextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);

        }

        //If user pressed 'Enter' key
        private void StackPanel_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 0);
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (ViewModel.LoginButtonAction())
                    Frame.Navigate(typeof(MainPage));
            }
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
        }
    }
}
