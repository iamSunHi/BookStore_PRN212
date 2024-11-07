using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Dialogs;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Pages;
using Microsoft.VisualBasic.Logging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BulkyBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserVM _userAuthen;
        private readonly IMapper _mapper;
        public MainWindow(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, IMapper mapper)
        {
            _mapper = mapper;
            _userAuthen = userAuthen;
            _unitOfWork = unitOfWork;
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            MainContent.Navigate(new HomePage(_unitOfWork, _userAuthen, _mapper));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginView = new LoginWindow(_unitOfWork, _mapper);
            loginView.Show();
            this.Close();
        }

        private void UserManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(new UserManagementPage(_unitOfWork, _userAuthen, _mapper));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(new HomePage(_unitOfWork, _userAuthen, _mapper));
        }

		private void CartButton_Click(object sender, RoutedEventArgs e)
		{
			MainContent.Navigate(new OrderCustomer(_unitOfWork, _userAuthen));
		}
    }
}