using AgendaCON.Models;
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
            MenuItems.Add(new NavigationViewItem() { Content = "", Icon = new SymbolIcon(Symbol.Contact), Tag = "user" });
            MenuItems.Add(new NavigationViewItemHeader { Content = "Calendar", Margin = new Thickness(33, 0, 0, 0) });
            MenuItems.Add(new NavigationViewItem() { Content = "My days", Icon = new SymbolIcon(Symbol.CalendarDay), Tag = "myday" });
            MenuItems.Add(new NavigationViewItem() { Content = "Expired tasks", Icon = new SymbolIcon(Symbol.CalendarWeek), Tag = "expired" });
            MenuItems.Add(new NavigationViewItem() { Content = "All tasks", Icon = new SymbolIcon(Symbol.CalendarReply), Tag = "alltasks" });
            MenuItems.Add(new NavigationViewItemSeparator());
            MenuItems.Add(new NavigationViewItemHeader { Content = "Categories", Margin = new Thickness(33, 0, 0, 0) });

            foreach (var category in categories)
            {
                SymbolIcon icon = new SymbolIcon();
                switch (category.CategoryType)
                {
                    case CategoryTypes.Checklist:
                        icon = new SymbolIcon(Symbol.AllApps);
                        break;
                    case CategoryTypes.Kanban3:
                        icon = new SymbolIcon(Symbol.DockBottom);
                        break;
                    case CategoryTypes.Kanban5:
                        icon = new SymbolIcon(Symbol.SelectAll);
                        break;
                    case CategoryTypes.MultiChecklist:
                        icon = new SymbolIcon(Symbol.Bookmarks);
                        break;
                }
                MenuItems.Add(new NavigationViewItem { Content = category.Name, Name = category.CategoryId.ToString(), Icon = icon, Tag = category.CategoryType.ToString() });
            }

            MenuItems.Add(new NavigationViewItemSeparator());
            MenuItems.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public void AddMenuItem(CategoryDto category)
        {
            SymbolIcon icon = new SymbolIcon();
            switch (category.CategoryType)
            {
                case CategoryTypes.Checklist:
                    icon = new SymbolIcon(Symbol.AllApps);
                    break;
                case CategoryTypes.Kanban3:
                    icon = new SymbolIcon(Symbol.DockBottom);
                    break;
                case CategoryTypes.Kanban5:
                    icon = new SymbolIcon(Symbol.SelectAll);
                    break;
                case CategoryTypes.MultiChecklist:
                    icon = new SymbolIcon(Symbol.Bookmarks);
                    break;
            }

            MenuItems.RemoveAt(MenuItems.Count - 1);
            MenuItems.RemoveAt(MenuItems.Count - 1);
            MenuItems.Add(new NavigationViewItem { Content = category.Name, Name = category.CategoryId.ToString(), Icon = icon, Tag = category.CategoryType.ToString() });
            MenuItems.Add(new NavigationViewItemSeparator());
            MenuItems.Add(new NavigationViewItem { Content = "Add new category", Icon = new SymbolIcon(Symbol.Add), Tag = "addnew" });
        }

        public void DeleteMenuItem(int id)
        {
            NavigationViewItemBase deletion = null;
            foreach (var item in MenuItems)
            {
                if (item.Name != null && item.Name == id.ToString())
                    deletion = item;
            }
            MenuItems.Remove(deletion);
        }

        public void RenameMenuItem(int id, string name)
        {
            foreach (var item in MenuItems)
            {
                if (item.Name != null && item.Name == id.ToString())
                    item.Content = name;
            }
        }

        public void ChangeMenuItemTag(int id, string newTag)
        {
            var item = MenuItems.Where(i => i.Name == id.ToString()).First();
            MenuItems[MenuItems.IndexOf(item)].Tag = newTag;
        }

        public void SetUserEmail(string email)
        {
            var item = MenuItems.Where(i => i.Tag.ToString() == "user").First();
            MenuItems[MenuItems.IndexOf(item)].Content = email;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }


}
