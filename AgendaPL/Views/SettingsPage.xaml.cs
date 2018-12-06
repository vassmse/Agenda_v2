using AgendaCON.Models;
using AgendaPL.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AgendaPL.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public SettingsPage()
        {
            this.InitializeComponent();
            ViewModel = vm.MainPage;
        }

        // Delete the category
        private void DeleteCategory(object sender, RoutedEventArgs e)
        {
            CategoryDto source = ((Button)sender).DataContext as CategoryDto;
            ViewModel.DeleteCategoryAction(source);
        }

        // Rename the category
        private void RenameCategory(object sender, RoutedEventArgs e)
        {
            CategoryDto source = ((Button)sender).DataContext as CategoryDto;
            ViewModel.RenameCategoryAction(source);
        }
    }
}
