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
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;

namespace BulkyBook.Dialogs
{
    /// <summary>
    /// Interaction logic for OrderDetailsDialog.xaml
    /// </summary>
    public partial class OrderDetailsDialog : UserControl
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderVM Order { get; set; }

        public OrderDetailsDialog(IUnitOfWork unitOfWork, OrderHeader orderHeader)
        {
            _unitOfWork = unitOfWork;

            Order = new OrderVM
            {
                OrderHeader = orderHeader,
                OrderDetail = _unitOfWork.OrderDetailRepository.Get(o => o.OrderHeaderId == orderHeader.Id, includeProperties: "Product")
            };

            InitializeComponent();

            DataContext = Order;
            ChangeStatusBtn();
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            Order.OrderHeader.OrderStatus = StaticDetails.StatusApproved;
            Order.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusApproved;
            Order.OrderHeader.PaymentDate = DateTime.Now;

            _unitOfWork.OrderHeaderRepository.Update(Order.OrderHeader);
            _unitOfWork.Save();

            ChangeStatusBtn();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Order.OrderHeader.OrderStatus = StaticDetails.StatusCancelled;
            Order.OrderHeader.PaymentStatus = StaticDetails.PaymentStatusRejected;
            Order.OrderHeader.PaymentDate = DateTime.Now;

            _unitOfWork.OrderHeaderRepository.Update(Order.OrderHeader);
            _unitOfWork.Save();

            ChangeStatusBtn();
        }

        private void btnProcessing_Click(object sender, RoutedEventArgs e)
        {
            Order.OrderHeader.OrderStatus = StaticDetails.StatusInProcess;

            _unitOfWork.OrderHeaderRepository.Update(Order.OrderHeader);
            _unitOfWork.Save();

            ChangeStatusBtn();
        }

        private void btnShipped_Click(object sender, RoutedEventArgs e)
        {
            Order.OrderHeader.OrderStatus = StaticDetails.StatusShipped;
            Order.OrderHeader.ShippingDate = DateTime.Now;

            _unitOfWork.OrderHeaderRepository.Update(Order.OrderHeader);
            _unitOfWork.Save();

            ChangeStatusBtn();
        }

        private void ChangeStatusBtn()
        {
            txtStatus.Text = Order.OrderHeader.OrderStatus;

            btnApprove.Visibility = Visibility.Hidden;
            btnProcessing.Visibility = Visibility.Hidden;
            btnShipped.Visibility = Visibility.Hidden;

            switch (Order.OrderHeader.OrderStatus)
            {
                case StaticDetails.PaymentStatusApproved:
                    btnProcessing.Visibility = Visibility.Visible;
                    txtStatus.Foreground = Brushes.DarkGreen;
                    break;
                case StaticDetails.StatusPending:
                    btnApprove.Visibility = Visibility.Visible;
                    txtStatus.Foreground = Brushes.DarkCyan;
                    break;
                case StaticDetails.StatusInProcess:
                    btnShipped.Visibility = Visibility.Visible;
                    txtStatus.Foreground = Brushes.DarkBlue;
                    break;
                case StaticDetails.StatusCancelled:
                    btnCancel.Visibility = Visibility.Hidden;
                    txtStatus.Foreground = Brushes.DarkRed;
                    break;
                case StaticDetails.StatusShipped:
                    btnCancel.Visibility = Visibility.Hidden;
                    txtStatus.Foreground = Brushes.DarkGoldenrod;
                    break;
            }
        }
    }
}
