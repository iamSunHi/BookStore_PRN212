using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BulkyBook.Dialogs;

namespace BulkyBook.Pages
{
    /// <summary>
    /// Interaction logic for OrderManagementPage.xaml
    /// </summary>
    public partial class OrderManagementPage : Page
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ObservableCollection<OrderHeader> OrderList { get; set; }

        public OrderManagementPage(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Load()
        {
            var orderHeaderList = _unitOfWork.OrderHeaderRepository.GetAll(includeProperties: "ApplicationUser");

            if (orderHeaderList is not null)
            {
                OrderList = new ObservableCollection<OrderHeader>(orderHeaderList);
                dgvOrder.ItemsSource = OrderList;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void dgvOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvOrder.SelectedItem is OrderHeader selectedOrder)
            {
                var dialog = new OrderDetailsDialog(_unitOfWork, selectedOrder);
                var window = new Window
                {
                    Title = "Order Details",
                    Content = dialog,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    ResizeMode = ResizeMode.NoResize
                };
                if (window.ShowDialog() == true)
                {
                    Load();
                }
            }
        }
    }
}
