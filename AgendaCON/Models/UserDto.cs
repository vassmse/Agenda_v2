using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCON.Models
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

        private string token;
        public string Token
        {
            get { return token; }
            set
            {
                token = value;
                NotifyPropertyChanged(nameof(Token));
            }
        }

        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set
            {
                isLoggedIn = value;
                NotifyPropertyChanged(nameof(IsLoggedIn));
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