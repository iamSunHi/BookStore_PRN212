using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using CredentialManagement;
using System.Windows;
using System.Windows.Navigation;

namespace BulkyBook
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mappers;

        public LoginWindow(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mappers = mapper;
            _unitOfWork = unitOfWork;
            InitializeComponent();
            LoadSavedCredentials();
        }

        private void LoadSavedCredentials()
        {
            using (var cred = new Credential { Target = "UserLogin" })
            {
                if (cred.Load())
                {
                    txtUserName.Text = cred.Username;
                    txtPassword.Password = cred.Password;
                    chkRememberMe.IsChecked = true;
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Password.Trim();

            ClearErrorMessages();
            bool isValid = true;
            if (string.IsNullOrEmpty(username))
            {
                txtUserNameError.Text = "User Name cannot be empty.";
                txtUserNameError.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                txtPasswordError.Text = "Password cannot be empty.";
                txtPasswordError.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (!isValid)
                return;

            var userAuthen = _mappers.Map<ApplicationUserVM>(_unitOfWork.ApplicationUserRepository.Get(u => u.UserName == username && u.Password == password, "Company"));
            if (userAuthen != null)
            {
                HandleCredentials(userAuthen);
                if (userAuthen.Role == "Customer")
                {
                    MainWindow mainWindow = new MainWindow(_unitOfWork, userAuthen, _mappers);
                    mainWindow.Show();
                }
                else
                {
                    AdminWindow adminWindow = new AdminWindow(_unitOfWork, userAuthen, _mappers);
                    adminWindow.Show();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }

        private void HandleCredentials(ApplicationUserVM userAuthen)
        {
            if (chkRememberMe.IsChecked == true)
            {
                SaveCredentials(userAuthen);
            }
            else
            {
                DeleteSavedCredentials();
            }
        }

        private void SaveCredentials(ApplicationUserVM userAuthen)
        {
            using (var cred = new Credential())
            {
                cred.Target = "UserLogin";
                cred.Username = userAuthen.UserName;
                cred.Password = userAuthen.Password;
                cred.Type = CredentialType.Generic;
                cred.PersistanceType = PersistanceType.LocalComputer;
                cred.Save();
            }
        }

        private void DeleteSavedCredentials()
        {
            using (var cred = new Credential { Target = "UserLogin" })
            {
                cred.Delete();
            }
        }

        private void ClearErrorMessages()
        {
            txtUserNameError.Visibility = Visibility.Collapsed;
            txtPasswordError.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ForgotPassword(object sender, RequestNavigateEventArgs e)
        {
            // Uncomment and implement the ForgotPassword logic
            // ForgotWindow forgotWindow = new ForgotWindow();
            // forgotWindow.Show();
        }

        private void Register(object sender, RequestNavigateEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(_unitOfWork);
            registerWindow.Show();
        }
    }
}
