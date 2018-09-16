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
                var categories = RestClient.GetAllCategories();

                foreach (var category in categories)
                {
                    foreach (var task in GetAllTasks())
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
                    foreach (var task in GetAllTasks())
                    {
                        if (category.CategoryId == task.ParentCategoryId && task.IsSubTask)
                        {
                            category.Tasks.FirstOrDefault(t => t.TaskId == task.ParentTaskId).SubTasks.Add(task);
                        }
                    }
                }

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
            ViewModel.SelectedCategory.Tasks.Add(task);
            ViewModel.CategoryCollection.AllTasks.Add(task);
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
}
