using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
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

        public LoginWindow(IUnitOfWork unitOfWork)
        {
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

            var userAuthen = _unitOfWork.ApplicationUserRepository.Get(u => u.UserName == username && u.Password == password);
            if (userAuthen != null)
            {
                HandleCredentials(userAuthen);
                MessageBox.Show("Login Successful!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }

        private void HandleCredentials(ApplicationUser userAuthen)
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

        private void SaveCredentials(ApplicationUser userAuthen)
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
