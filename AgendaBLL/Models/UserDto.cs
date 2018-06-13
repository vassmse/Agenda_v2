using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaBLL.Models
{
    public class UserDto : INotifyPropertyChanged
    {
        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                NotifyPropertyChanged(nameof(UserId));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private string passwordHash;
        public string PasswordHash
        {
            get { return passwordHash; }
            set
            {
                passwordHash = value;
                NotifyPropertyChanged(nameof(PasswordHash));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }

        private DateTime registerDate;
        public DateTime RegisterDate
        {
            get { return registerDate; }
            set
            {
                registerDate = value;
                NotifyPropertyChanged(nameof(RegisterDate));
            }
        }

        private List<CategoryDto> categories;

        public List<CategoryDto> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                NotifyPropertyChanged(nameof(Categories));
            }
        }

        public UserDto()
        {
            Categories = new List<CategoryDto>();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}