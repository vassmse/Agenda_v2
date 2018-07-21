using AgendaBLL.Models;
using AgendaPL.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPL.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        public List<CategoryDto> Categories { get; set; }

        private AgendaRestClient RestClient { get; set; }

        public MainViewModel()
        {
            RestClient = new AgendaRestClient();

            Categories = RestClient.GetAllCategories();
        }
    }
}
