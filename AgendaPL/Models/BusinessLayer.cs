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
                            category.Tasks.Add(task);
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
            var newCategory = new CategoryDto {Name = ViewModel.NewCategory.Name, CategoryType = ViewModel.NewCategory.CategoryType };
            ViewModel.CategoryCollection.Categories.Add(newCategory);
            RestClient.AddCategory(category);
            ViewModel.NavigationViewItems.AddMenuItem(newCategory);
            ViewModel.NewCategory.Name = String.Empty;
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
    }
}
