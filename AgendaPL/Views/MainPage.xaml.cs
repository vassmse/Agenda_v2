using AgendaPL.Models;
using AgendaPL.ViewModels;
using AgendaPL.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        CommandBar commandBar = new CommandBar();
        List<AppBarButton> appBarButtons = new List<AppBarButton>();

        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = vm.MainPage;

        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            NavView.SelectedItem = ViewModel.NavigationViewItems.MenuItems[1];
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
                NavView.Header = item.Content;
                ViewModel.SelectedCategory = ViewModel.CategoryCollection.Categories.FirstOrDefault(c => c.Name == item.Content.ToString());

                if (item.Name.Length > 0)
                    commandBar.Visibility = Visibility.Visible;
                else
                    commandBar.Visibility = Visibility.Collapsed;
                

                switch (item.Tag)
                {
                    case "today":
                        ContentFrame.Navigate(typeof(DailyReportPage));
                        break;
                    case "week":
                        ContentFrame.Navigate(typeof(WeeklyReportPage));
                        break;
                    case "expired":
                        ContentFrame.Navigate(typeof(ExpiredTasksPage));
                        break;

                    case "Checklist":
                        ContentFrame.Navigate(typeof(CheckListPage));
                        changeAppBarEnabling("Checklist");
                        break;
                    case "MultiChecklist":
                        ContentFrame.Navigate(typeof(MultiCheckListPage));
                        changeAppBarEnabling("Multichecklist");
                        break;
                    case "Kanban3":
                        ContentFrame.Navigate(typeof(KanbanPage));
                        changeAppBarEnabling("Kanban3");
                        break;
                    case "Kanban5":
                        ContentFrame.Navigate(typeof(KanbanExtendedPage));
                        changeAppBarEnabling("Kanban5");
                        break;
                    case "addnew":
                        ContentFrame.Navigate(typeof(NewCategoryPage));
                        break;
                    default:
                        ContentFrame.Navigate(typeof(SettingsPage));
                        break;
                }                

            }

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            int parameter = Convert.ToInt32(((AppBarButton)sender).CommandParameter);
            if ((NavView.SelectedItem as NavigationViewItem).Name.Length > 0)
            {
                ViewModel.ChangeCategoryTypeAction(parameter);
                var selectedItem = NavView.SelectedItem as NavigationViewItem;
                NavView_Navigate(selectedItem);
            }

        }

        private void CommandBar_Loaded(object sender, RoutedEventArgs e)
        {
            commandBar = (CommandBar)sender;
            commandBar.Visibility = Visibility.Collapsed;
        }

        private void ChecklistBarLoaded(object sender, RoutedEventArgs e)
        {
            appBarButtons.Add((AppBarButton)sender);
        }

        private void MultiChecklistBarLoaded(object sender, RoutedEventArgs e)
        {
            appBarButtons.Add((AppBarButton)sender);
        }

        private void Kanban3BarLoaded(object sender, RoutedEventArgs e)
        {
            appBarButtons.Add((AppBarButton)sender);
        }

        private void Kanban5BarLoaded(object sender, RoutedEventArgs e)
        {
            appBarButtons.Add((AppBarButton)sender);
        }

        private void changeAppBarEnabling(string label)
        {
            foreach(var button in appBarButtons )
            {
                if (button.Label == label)
                    button.IsEnabled = false;
                else
                    button.IsEnabled = true;

            }
        }
    }
}
