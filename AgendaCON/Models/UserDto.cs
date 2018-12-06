using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCON.Models
{
    // Data Transfer Object for Users
    public class UserDto : INotifyPropertyChanged
    {
        //Id of the user
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

        //Password hash for the user
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

        //Email address for the user
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

        //Token in JWT
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

        //Is the user logged in
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

        //Categories of the user
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