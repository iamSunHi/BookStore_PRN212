using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using BulkyBook.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BulkyBook
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserVM _userAuthen;
        private readonly IMapper _mapper;
        public AdminWindow(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userAuthen = userAuthen;
            _mapper = mapper;
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            MainContent.Navigate(new ProductManagementPage(_unitOfWork, _mapper));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginView = new LoginWindow(_unitOfWork, _mapper);
            loginView.Show();
            this.Close();
        }

        private void CategoryManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(new CategoryManagementPage(_unitOfWork, _mapper));
        }

        private void ProductManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(new ProductManagementPage(_unitOfWork, _mapper));
        }

        private void CoverTypeManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(new CoverTypeManagementPage(_unitOfWork, _mapper));
        }

        private void OrderManagement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Navigate(new OrderManagementPage(_unitOfWork, _mapper));
        }
    }
}
