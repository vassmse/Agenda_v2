using AgendaPL.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPL.Views.UserControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public RegisterPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.OkMessage = String.Empty;
            ViewModel.ErrorMessage = String.Empty;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Register();
        }

        private void LoginButtonTextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void LoginButtonTextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
        }

        private void LoginButtonTextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);

        }
    }
}
