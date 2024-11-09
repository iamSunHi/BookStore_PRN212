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

namespace BulkyBook.Dialogs
{
	/// <summary>
	/// Interaction logic for OrderCustomer.xaml
	/// </summary>
	public partial class OrderCustomer : Page
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationUserVM _userAuthen;

		public OrderCustomer(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen)
		{
			_unitOfWork = unitOfWork;
			_userAuthen = userAuthen;
			InitializeComponent();
			LoadOrder();
		}

		private void LoadOrder()
		{
			var orderHeaders = _unitOfWork.OrderHeaderRepository
				.GetAll(u => u.ApplicationUserId == _userAuthen.Id);

			if (orderHeaders != null && orderHeaders.Any())
			{
				var allOrderDetails = new List<OrderDetail>();
				foreach (var order in orderHeaders)
				{
					var orderDetails = _unitOfWork.OrderDetailRepository
						.GetAll(u => u.OrderHeaderId == order.Id, includeProperties: "Product,OrderHeader")
						.ToList();

					if (orderDetails != null)
					{
						allOrderDetails.AddRange(orderDetails);
					}
				}
				dataGrid.ItemsSource = allOrderDetails;
			}
			else
			{
				MessageBox.Show("You do not have any order yet");
			}
		}

	}
}
