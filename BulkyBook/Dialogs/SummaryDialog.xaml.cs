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
	/// Interaction logic for SummaryDialog.xaml
	/// </summary>
	public partial class SummaryDialog : Window
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationUserVM _userAuthen;
		public ProductVM Product { get; set; }
		public OrderHeaderVM OrderHeader { get; set; }

		public SummaryDialog(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, ProductVM product)
		{
			_unitOfWork = unitOfWork;
			_userAuthen = userAuthen;
			Product = product;

			OrderHeader = new OrderHeaderVM
			{
				Name = _userAuthen.Name,
				StreetAddress = _userAuthen.Address,
				City = _userAuthen.City,
				State = _userAuthen.State,
				PostalCode = _userAuthen.PostalCode,
				ProductVM = product
			};

			InitializeComponent();
			DataContext = this;
		}

		private void btnSummary_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!CheckField())
				{
					return;
				}

				var orderHeader = new OrderHeader
				{
					Address = OrderHeader.StreetAddress,
					Name = OrderHeader.Name,
					City = OrderHeader.City,
					State = OrderHeader.State,
					PhoneNumber = txtPhone.Text,
					PostalCode = OrderHeader.PostalCode,
					OrderDate = DateTime.Now,
					OrderTotal = Product.Quantity * Product.Product.Price,
					ApplicationUserId = _userAuthen.Id
				};

				if (_userAuthen.CompanyId.GetValueOrDefault() == 0)
				{
					// Regular customer
					orderHeader.PaymentStatus = StaticDetails.PaymentStatusPending;
					orderHeader.OrderStatus = StaticDetails.StatusPending;
				}
				else
				{
					// Company customer
					orderHeader.PaymentStatus = StaticDetails.PaymentStatusDelayedPayment;
					orderHeader.OrderStatus = StaticDetails.StatusApproved;
				}

				_unitOfWork.OrderHeaderRepository.Add(orderHeader);
				_unitOfWork.Save();

				var orderDetail = new OrderDetail
				{
					ProductId = Product.Product.Id,
					OrderHeaderId = orderHeader.Id,
					Price = Product.Product.Price,
					Count = Product.Quantity,
				};
				_unitOfWork.OrderDetailRepository.Add(orderDetail);
				_unitOfWork.Save();

				MessageBox.Show("Order has been saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

				this.DialogResult = true;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while saving the order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private bool CheckField()
		{
			if (Product == null || Product.Product == null)
			{
				MessageBox.Show("Product information is missing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(OrderHeader.Name))
			{
				MessageBox.Show("Please enter a name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtPhone.Text))
			{
				MessageBox.Show("Please enter a phone number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(OrderHeader.StreetAddress))
			{
				MessageBox.Show("Please enter a street address.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(OrderHeader.City))
			{
				MessageBox.Show("Please enter a city.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(OrderHeader.State))
			{
				MessageBox.Show("Please enter state.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			if (string.IsNullOrWhiteSpace(OrderHeader.PostalCode))
			{
				MessageBox.Show("Please enter a postal code.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
			return true;
		}

	}
}
