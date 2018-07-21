﻿using AgendaBLL.Models;
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
            var categories = RestClient.GetAllCategories();
            return new ObservableCollection<CategoryDto>(categories);
        }

        public void AddCategory(CategoryDto category)
        {
            int id = ViewModel.Categories.Last().CategoryId++;
            var newCategory = new CategoryDto { CategoryId = id, Name = ViewModel.NewCategory.Name, CategoryType = ViewModel.NewCategory.CategoryType };
            ViewModel.Categories.Add(newCategory);
            ViewModel.NavigationViewItems.AddMenuItem(newCategory);
            ViewModel.NewCategory.Name = String.Empty;
        }
    }
}
