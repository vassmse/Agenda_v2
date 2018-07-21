using AgendaPL.ViewModels;
using AgendaPL.Views;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AgendaPL
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = vm.MainPage;
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            NavView_Navigate(item);
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            if (item != null)
            {
                switch (item.Tag)
                {
                    case "today":
                        ContentFrame.Navigate(typeof(DailyReportPage));
                        NavView.Header = "Mai nap";
                        break;
                    case "week":
                        ContentFrame.Navigate(typeof(WeeklyReportPage));
                        NavView.Header = "Heti jelentés";
                        break;
                    case "addnew":
                        ContentFrame.Navigate(typeof(NewCategoryPage));
                        NavView.Header = "Add new category";
                        break;

                    default:
                        break;

                }
            }
        }


    }
}
