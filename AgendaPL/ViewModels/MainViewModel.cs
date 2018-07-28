using AgendaCON.Models;
using AgendaPL.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;

namespace AgendaPL.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region RelayCommand properties

        public RelayCommand AddCategoryCommand { get; private set; }

        #endregion

        public CategoryCollection CategoryCollection { get; set; }

        private BusinessLayer businessLayer { get; set; }

        public NavigationViewMenuItems NavigationViewItems { get; set; }

        public CategoryDto NewCategory { get; set; }

        private CategoryDto selectedCategory;

        public CategoryDto SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }



        public MainViewModel()
        {
            businessLayer = new BusinessLayer(this);
            NewCategory = new CategoryDto();
            CategoryCollection = new CategoryCollection();
            CategoryCollection.Categories = businessLayer.GetAllCategories();
            NavigationViewItems = new NavigationViewMenuItems(CategoryCollection.Categories);
            SelectedCategory = new CategoryDto();
            

            #region RelayCommands

            AddCategoryCommand = new RelayCommand(AddCategoryAction);

            #endregion

        }

        #region Commands

        private void AddCategoryAction()
        {
            businessLayer.AddCategory(NewCategory);
        }

        #endregion

    }
}
