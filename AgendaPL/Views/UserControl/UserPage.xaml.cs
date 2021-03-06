﻿using AgendaPL.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public UserPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
        }

        // User logout
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Logout();
            var parent = this.Frame.Parent as NavigationView;
            var grid = parent.Parent as Grid;
            var mainpage = grid.Parent as MainPage;
            var mainframe = mainpage.Parent as Frame;
            mainframe.Navigate(typeof(LoginPage));
        }
    }
}
