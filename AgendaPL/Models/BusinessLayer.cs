using AgendaCON.Models;
using AgendaPL.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPL.Models
{
    public class BusinessLayer
    {
        private AgendaRestClient RestClient { get; set; }
        private MainViewModel ViewModel { get; set; }

        public BusinessLayer(MainViewModel viewModel)
        {
            RestClient = new AgendaRestClient();
            ViewModel = viewModel;
        }

        public ObservableCollection<CategoryDto> GetAllCategories()
        {
            try
            {
                //TODO
                var categories = RestClient.GetAllCategories();
                var tasks = GetAllTasks();
                ViewModel.CategoryCollection.LastId = tasks.Count + 1;

                foreach (var category in categories)
                {
                    foreach (var task in tasks)
                    {
                        if (category.CategoryId == task.ParentCategoryId)
                        {
                            if (!task.IsSubTask)
                                category.Tasks.Add(task);
                        }
                    }
                }

                //SubTasks
                foreach (var category in categories)
                {
                    foreach (var task in tasks)
                    {
                        if (category.CategoryId == task.ParentCategoryId && task.IsSubTask)
                        {
                            category.Tasks.FirstOrDefault(t => t.TaskId == task.ParentTaskId).SubTasks.Add(task);
                        }
                    }
                }
                var a = categories;

                return new ObservableCollection<CategoryDto>(categories);
            }
            catch (Exception)
            {
                return new ObservableCollection<CategoryDto>();
            }
        }

        public void AddCategory(CategoryDto category)
        {
            var newCategory = new CategoryDto { Name = ViewModel.NewCategory.Name, CategoryType = ViewModel.NewCategory.CategoryType };
            ViewModel.CategoryCollection.Categories.Add(newCategory);
            RestClient.AddCategory(category);
            ViewModel.NavigationViewItems.AddMenuItem(newCategory);
            ViewModel.NewCategory.Name = String.Empty;
        }

        public void DeleteCategory(CategoryDto category)
        {
            ViewModel.CategoryCollection.Categories.Remove(category);
            RestClient.DeleteCategory(category);
            ViewModel.NavigationViewItems.DeleteMenuItem(category.Name);
        }

        public void AddTask(TaskDto task)
        {
            RestClient.AddTask(task);

            if (task.IsSubTask)
                ViewModel.CategoryCollection.Categories.Where(c => c.CategoryId == task.ParentCategoryId).First().Tasks.Where(t => t.TaskId == task.ParentTaskId).First().SubTasks.Add(task);
            else
            {
                ViewModel.CategoryCollection.Categories.Where(c => c.CategoryId == task.ParentCategoryId).First().Tasks.Add(task);

            }
        }

        public ObservableCollection<TaskDto> GetAllTasks()
        {
            try
            {
                var tasks = RestClient.GetAllTasks();
                return new ObservableCollection<TaskDto>(tasks);
            }
            catch (Exception)
            {
                return new ObservableCollection<TaskDto>();
            }
        }

        public void UpdateTask(TaskDto task)
        {
            RestClient.UpdateTask(task);
            if (ViewModel.SelectedTask != null)
            {
                for (int i = 0; i < ViewModel.CategoryCollection.Categories.Count(); i++)
                {
                    if (ViewModel.CategoryCollection.Categories[i] == ViewModel.SelectedCategory)
                    {
                        for (int j = 0; j < ViewModel.CategoryCollection.Categories[i].Tasks.Count(); j++)
                        {
                            if (ViewModel.CategoryCollection.Categories[i].Tasks[j].TaskId == ViewModel.SelectedTask.TaskId)
                            {
                                ViewModel.CategoryCollection.Categories[i].Tasks[j] = ViewModel.SelectedTask;
                            }
                            else
                            {
                                for (int k = 0; k < ViewModel.CategoryCollection.Categories[i].Tasks[j].SubTasks.Count(); k++)
                                {
                                    if (ViewModel.CategoryCollection.Categories[i].Tasks[j].SubTasks[k].TaskId == ViewModel.SelectedTask.TaskId)
                                    {
                                        ViewModel.CategoryCollection.Categories[i].Tasks[j].SubTasks[k] = ViewModel.SelectedTask;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public void DeleteTask(TaskDto task)
        {
            RestClient.DeleteTask(task);
            ViewModel.IsPanelActive = false;
            ViewModel.CategoryCollection.Categories.Where(c => c.CategoryId == task.ParentCategoryId).FirstOrDefault().Tasks.Remove(task);
        }
    }
}
