using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Models.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class ApplicationUserVM : INotifyPropertyChanged
    {
        private string _id = null!;
        private string _userName = null!;
        private string? _email;
        private string _password = null!;
        private string _name = null!;
        private string? _address;
        private string? _city;
        private string? _state;
        private string? _postalCode;
        private int? _companyId;
        private CompanyVM? _company;
        private string _role = null!;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string? Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string? State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        public string? PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        public int? CompanyId
        {
            get => _companyId;
            set
            {
                _companyId = value;
                OnPropertyChanged();
            }
        }

        public CompanyVM? Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged();
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
