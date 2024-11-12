using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace BulkyBook.Pages
{
    /// <summary>
    /// Interaction logic for UserManagementPage.xaml
    /// </summary>
    public partial class UserManagementPage : Page
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserVM _userAuthen;
        private readonly IMapper _mapper;
        private ApplicationUserVM _userProfile;
        private bool isEdit;

        public UserManagementPage(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, IMapper mapper)
        {
            _mapper = mapper;
            _userAuthen = userAuthen;
            _unitOfWork = unitOfWork;
            InitializeComponent();
            LoadCompany();
            LoadUserProfile();
            isEdit = false;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            isEdit = !isEdit;
            SetTextBoxesEnabled(isEdit);
            SaveButton.IsEnabled = isEdit;
            LoadUserProfile();
        }

        private void LoadCompany()
        {
            var companies = _unitOfWork.CompanyRepository.GetAll().ToList();
            cboCompany.ItemsSource = _mapper.Map<List<CompanyVM>>(companies);
            cboCompany.DisplayMemberPath = "Name";
            cboCompany.SelectedValuePath = "Id";
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(pwdPassword.Password.IsNullOrEmpty())
            {
                _userProfile.Password = _userAuthen.Password;
            }
            else
            {
                _userProfile.Password = pwdPassword.Password;
            }
            if (!string.IsNullOrEmpty(_userProfile.Email) && !ValidateEmail(_userProfile.Email))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _userProfile.Company = null;
            _unitOfWork.ApplicationUserRepository.Update(_mapper.Map<ApplicationUser>(_userProfile));
            _unitOfWork.Save();
            SaveButton.IsEnabled = false;
            SetTextBoxesEnabled(false);
            LoadUserProfile();
        }

        private void SetTextBoxesEnabled(bool isEnabled)
        {
            pwdPassword.IsEnabled = isEnabled;
            txtEmail.IsEnabled = isEnabled;
            txtName.IsEnabled = isEnabled;
            txtAddress.IsEnabled = isEnabled;
            txtCity.IsEnabled = isEnabled;
            txtState.IsEnabled = isEnabled;
            txtPostalCode.IsEnabled = isEnabled;
            cboCompany.IsEnabled = isEnabled;
            pwdPassword.Clear();
        }

        private void LoadUserProfile()
        {
            var userProfile = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == _userAuthen.Id, "Company");
            _userProfile = _mapper.Map<ApplicationUserVM>(userProfile);
            DataContext = _userProfile;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserProfile();
        }
        public bool ValidateEmail(string email)
        {
            // Regular expression for validating an email
            string emailPattern = @"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$";

            // Create a regex object
            Regex regex = new Regex(emailPattern);

            // Return whether the email matches the pattern
            return regex.IsMatch(email);
        }
    }
}
