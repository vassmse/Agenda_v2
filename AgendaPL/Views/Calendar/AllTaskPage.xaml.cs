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
    public sealed partial class AllTaskPage : Page
    {
        ViewModelLocator vm = new ViewModelLocator();
        public MainViewModel ViewModel { get; set; }

        public AllTaskPage()
        {
            InitializeComponent();
            ViewModel = vm.MainPage;
        }

        // Task ticked or deticked
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            TaskDto source = ((CheckBox)sender).DataContext as TaskDto;
            ViewModel.CheckChangedAction(source);
        }
    }
}
