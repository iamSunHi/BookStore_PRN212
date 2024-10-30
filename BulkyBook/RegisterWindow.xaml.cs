using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BulkyBook
{
    public partial class RegisterWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterWindow(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorMessages();

            bool hasErrors = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                SetError(txtNameError, "Name cannot be empty.");
                hasErrors = true;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                SetError(txtUserNameError, "User name cannot be empty.");
                hasErrors = true;
            }
            else if (_unitOfWork.ApplicationUserRepository.Get(u => u.UserName == txtUserName.Text) != null)
            {
                SetError(txtUserNameError, "User name already exists.");
                hasErrors = true;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                SetError(txtPasswordError, "Password cannot be empty.");
                hasErrors = true;
            }

            if (string.IsNullOrWhiteSpace(txtRepassword.Password))
            {
                SetError(txtRepasswordError, "Re-enter password cannot be empty.");
                hasErrors = true;
            }
            else if (txtPassword.Password != txtRepassword.Password)
            {
                SetError(txtRepasswordError, "Re-entered password does not match.");
                hasErrors = true;
            }

            if (!hasErrors)
            {
                var registerUser = new RegisterUserVM
                {
                    ApplicationUser = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = txtUserName.Text,
                        Password = txtPassword.Password,
                        Name = txtName.Text,
                        Role = "Customer"
                    }
                };
                _unitOfWork.ApplicationUserRepository.Add(registerUser.ApplicationUser);
                _unitOfWork.Save();
                MessageBox.Show("Register Successfull!");
                this.Close();
            }
        }

        private void ClearErrorMessages()
        {
            txtNameError.Visibility = Visibility.Collapsed;
            txtUserNameError.Visibility = Visibility.Collapsed;
            txtPasswordError.Visibility = Visibility.Collapsed;
            txtRepasswordError.Visibility = Visibility.Collapsed;
        }

        private void SetError(TextBlock errorTextBlock, string message)
        {
            errorTextBlock.Text = message;
            errorTextBlock.Visibility = Visibility.Visible;
        }
    }
}
