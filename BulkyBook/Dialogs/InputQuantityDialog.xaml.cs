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
using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using MaterialDesignThemes.Wpf;

namespace BulkyBook.Dialogs
{
	/// <summary>
	/// Interaction logic for InputQuantityDialog.xaml
	/// </summary>
	public partial class InputQuantityDialog : Window
	{
		public int Quantity { get; private set; }

		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationUserVM _userAuthen;
		private readonly IMapper _mapper;
		private readonly Product _product;


		public InputQuantityDialog(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, IMapper mapper, Product product)
		{
			_mapper = mapper;
			_userAuthen = userAuthen;
			_unitOfWork = unitOfWork;
			_product = product;
			InitializeComponent();
			DataContext = this;
		}

		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
			{
				Quantity = quantity;
				DialogResult = true;

				var productVM = new ProductVM
				{
					Quantity = Quantity,
					Product = _product,
				};

				SummaryDialog summaryDialog = new SummaryDialog(_unitOfWork, _userAuthen, productVM);
				summaryDialog.ShowDialog();
			}
			else
			{
				MessageBox.Show("Vui lòng nhập số lượng hợp lệ.");
			}
		}

	}
}
