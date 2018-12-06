using AgendaCON.Models;
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

namespace AgendaPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CheckListPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public CheckListPage()
        {
            InitializeComponent();
            ViewModel = vm.MainPage;
        }

        // Task selected by user to modify
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedTask = e.ClickedItem as TaskDto;
            ViewModel.SelectedTaskAction(selectedTask);
        }

        // Task ticked
        private void TaskReady(object sender, RoutedEventArgs e)
        {
            TaskDto source = ((CheckBox)sender).DataContext as TaskDto;
            ViewModel.CheckChangedAction(source);
        }

        // Adding new task
        private void AddNewTask(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.AddNewTask();
        }
    }
}
