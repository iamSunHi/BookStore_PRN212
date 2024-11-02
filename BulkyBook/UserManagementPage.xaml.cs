using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BulkyBook
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
            txtName.IsEnabled = isEnabled;
            txtAddress.IsEnabled = isEnabled;
            txtCity.IsEnabled = isEnabled;
            txtState.IsEnabled = isEnabled;
            txtPostalCode.IsEnabled = isEnabled;
            cboCompany.IsEnabled = isEnabled;
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
    }
}
