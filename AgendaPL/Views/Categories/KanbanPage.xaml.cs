﻿using AgendaCON.Models;
using AgendaPL.ViewModels;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KanbanPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public KanbanPage()
        {
            InitializeComponent();
            ViewModel = vm.MainPage;
            DataContext = ViewModel;
        }

        // Drop task to a state column
        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if (((e.OriginalSource as ScrollViewer)?.DataContext is MainViewModel targetAccount) && (e.OriginalSource as ScrollViewer).Name != null)
                if (await (e.DataView.GetDataAsync("ID")) is int taskId)
                {
                    try
                    {
                        var targetState = (e.OriginalSource as ScrollViewer)?.Name;
                        switch (targetState)
                        {
                            case "BacklogPanel":
                                ViewModel.ChangeTaskState(taskId, 0);
                                break;
                            case "InProgressPanel":
                                ViewModel.ChangeTaskState(taskId, 2);
                                break;
                            case "DonePanel":
                                ViewModel.ChangeTaskState(taskId, 4);
                                break;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine(ex); }
                }
        }

        // Moving is over
        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
            e.DragUIOverride.IsGlyphVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsCaptionVisible = true;
        }

        // Starts to move a task
        private void TextBlock_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            if ((sender as StackPanel)?.DataContext is TaskDto task)
            {
                args.AllowedOperations = DataPackageOperation.Move;
                args.Data.SetData("ID", task.TaskId);
            }
        }

        private void ItemsControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);

        }

        private void ItemsControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);

        }

        // Adding new task
        private void AddNewTask(object sender, PointerRoutedEventArgs e)
        {
            ViewModel.AddNewTask();
        }

        // Select a task for modifying
        private void doneControl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            TaskDto selectedTask;
            if (e.OriginalSource as TextBlock == null)
            {
                var source = e.OriginalSource as StackPanel;
                selectedTask = source.DataContext as TaskDto;
            }
            else
            {
                var source = e.OriginalSource as TextBlock;
                selectedTask = source.DataContext as TaskDto;
            }

            ViewModel.SelectedTaskAction(selectedTask);
        }
    }
}
