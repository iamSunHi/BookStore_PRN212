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
    /// Interaction logic for BookManagementPage.xaml
    /// </summary>
    public partial class ProductManagementPage : Page
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ObservableCollection<Product> ProductList { get; set; }

        public ProductManagementPage(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialog(_unitOfWork);
            var window = new Window
            {
                Title = "Add New Product",
                Content = dialog,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            dialog.Load();

            if (window.ShowDialog() == true)
            {
                var product = dialog.Product;
                _unitOfWork.ProductRepository.Add(_mapper.Map<Product>(product.Product));
                _unitOfWork.Save();
                Load();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvProduct.SelectedItem is Product selectedProduct)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selectedProduct.Title}?",
                                              "Confirm Delete",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _unitOfWork.ProductRepository.Remove(_mapper.Map<Product>(dgvProduct.SelectedItem));
                        _unitOfWork.Save();

                        MessageBox.Show("Delete successful.", "Delete Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                        Load();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Delete failed: " + ex.Message, "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Load()
        {
            var productList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category,CoverType");

            if (productList is not null)
            {
                ProductList = new ObservableCollection<Product>(productList);
                dgvProduct.ItemsSource = ProductList;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void dgvProduct_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvProduct.SelectedItem is Product selectedProduct)
            {
                var dialog = new ProductDialog(_unitOfWork);
                dialog.Product.Product = selectedProduct;
                var window = new Window
                {
                    Title = "Update Product",
                    Content = dialog,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    ResizeMode = ResizeMode.NoResize
                };
                dialog.Load();
                if (window.ShowDialog() == true)
                {
                    selectedProduct.Title = dialog.Product.Product.Title;
                    selectedProduct.Description = dialog.Product.Product.Description;
                    selectedProduct.ISBN = dialog.Product.Product.ISBN;
                    selectedProduct.Author = dialog.Product.Product.Author;
                    selectedProduct.Price = dialog.Product.Product.Price;
                    selectedProduct.ImageUrl = dialog.Product.Product.ImageUrl;
                    selectedProduct.CategoryId = dialog.Product.Product.CategoryId;
                    selectedProduct.Category = dialog.Product.Product.Category;
                    selectedProduct.CoverTypeId = dialog.Product.Product.CoverTypeId;
                    selectedProduct.CoverType = dialog.Product.Product.CoverType;

                    _unitOfWork.ProductRepository.Update(_mapper.Map<Product>(selectedProduct));
                    _unitOfWork.Save();

                    Load();
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                var productList = this.ProductList.Where(p => p.Title.ToLower().Contains(txtSearch.Text.ToLower()));
                dgvProduct.ItemsSource = new ObservableCollection<Product>(productList);
            }
            else
            {
                dgvProduct.ItemsSource = ProductList;
            }
        }
    }
}
