using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Dialogs;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;

namespace BulkyBook.UserControls
{
    /// <summary>
    /// Interaction logic for RoomCard.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationUserVM _userAuthen;
		private readonly IMapper _mapper;
		public ProductUserControl(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, IMapper mapper)
		{
			_mapper = mapper;
			_userAuthen = userAuthen;
			_unitOfWork = unitOfWork;
			InitializeComponent();
		}

		private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
			var product = DataContext as Product;

			if (product != null)
			{
				var inputQuantityWindow = new InputQuantityDialog(_unitOfWork, _userAuthen, _mapper, product);
				if (inputQuantityWindow.ShowDialog() == true)
				{
					int quantity = inputQuantityWindow.Quantity;
				}
			}
			else
			{
				MessageBox.Show("Can not get product information.");
			}
		}

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            int productId = int.Parse(txtRoomID.Text);
            var product = _unitOfWork.ProductRepository.Get(p => p.Id == productId, includeProperties: "Category,CoverType");
            var detailDialog = new Window
            {
                Title = "Product Details",
                Content = new UserProductDialog(product),
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            detailDialog.ShowDialog(); // Hiển thị cửa sổ

        }

        //public Product Product
        //{
        //    get { return (Product)GetValue(ProductProperty); }
        //    set { SetValue(ProductProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ProductProperty =
        //    DependencyProperty.Register("Product", typeof(Product), typeof(ProductUserControl), new PropertyMetadata(new Product()));


    }
}
