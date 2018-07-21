using AgendaBLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AgendaPL.Models
{
    public class NavigationViewMenuItems : INotifyPropertyChanged
    {
        public ObservableCollection<NavigationViewItemBase> MenuItems = new ObservableCollection<NavigationViewItemBase>();

        public NavigationViewMenuItems(ObservableCollection<CategoryDto> categories)
        {
            MenuItems.Add(new NavigationViewItemHeader { Content = "Calendar", Margin = new Thickness(33, 0, 0, 0) });
            MenuItems.Add(new NavigationViewItem() { Content = "Today", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "today" });
            MenuItems.Add(new NavigationViewItem() { Content = "My week", Icon = new SymbolIcon(Symbol.CalendarWeek), Tag = "week" });
            MenuItems.Add(new NavigationViewItem() { Content = "Expired", Icon = new SymbolIcon(Symbol.CalendarReply), Tag = "expired" });
            MenuItems.Add(new NavigationViewItemSeparator());
            MenuItems.Add(new NavigationViewItemHeader { Content = "Categories", Margin = new Thickness(33, 0, 0, 0) });

            foreach (var category in categories)
            {
                SymbolIcon icon = new SymbolIcon();
                switch (category.CategoryType)
                {
                    case StateTypes.Checklist:
                        icon = new SymbolIcon(Symbol.AllApps);
                        break;
                    case StateTypes.Kanban3:
                        icon = new SymbolIcon(Symbol.DockBottom);
                        break;
                    case StateTypes.Kanban5:
                        icon = new SymbolIcon(Symbol.SelectAll);
                        break;
                    case StateTypes.MultiChecklist:
                        icon = new SymbolIcon(Symbol.Bookmarks);
                        break;
                }
                MenuItems.Add(new NavigationViewItem { Content = category.Name, Icon = icon, Tag = category.CategoryType.ToString() });
            }

            MenuItems.Add(new NavigationViewItemSeparator());
            MenuItems.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public void AddMenuItem(CategoryDto category)
        {
            SymbolIcon icon = new SymbolIcon();
            switch (category.CategoryType)
            {
                case StateTypes.Checklist:
                    icon = new SymbolIcon(Symbol.AllApps);
                    break;
                case StateTypes.Kanban3:
                    icon = new SymbolIcon(Symbol.DockBottom);
                    break;
                case StateTypes.Kanban5:
                    icon = new SymbolIcon(Symbol.SelectAll);
                    break;
                case StateTypes.MultiChecklist:
                    icon = new SymbolIcon(Symbol.Bookmarks);
                    break;
            }

            MenuItems.RemoveAt(MenuItems.Count - 1);
            MenuItems.RemoveAt(MenuItems.Count - 1);
            MenuItems.Add(new NavigationViewItem { Content = category.Name, Icon = icon, Tag = category.CategoryType.ToString() });
            MenuItems.Add(new NavigationViewItemSeparator());
            MenuItems.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public void DeleteMenuItem(string name)
        {
            NavigationViewItemBase deletion = null;
            foreach (var item in MenuItems)
            {
                if (item.Content != null && item.Content.ToString() == name)
                    deletion = item;
            }
            MenuItems.Remove(deletion);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }


}
