using AutoMapper;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.UserControls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace BulkyBook.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserVM _userAuthen;
        private readonly IMapper _mapper;

        public HomePage(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, IMapper mapper)
        {
            _mapper = mapper;
            _userAuthen = userAuthen;
            _unitOfWork = unitOfWork;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var productList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category,CoverType");
            LoadDataCategoryAndCoverType();
            LoadDataToDataGrid(new ObservableCollection<Product>(productList.Take(16)));
        }

        public void LoadDataCategoryAndCoverType()
        {

            var categories = _unitOfWork.CategoryRepository.GetAll().ToList(); 

            categories.Insert(0, new Category { Id = 0, Name = "None" });

            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "Id";

            var coverTypes = _unitOfWork.CoverTypeRepository.GetAll().ToList(); 

            coverTypes.Insert(0, new CoverType { Id = 0, Name = "None" });

            CoverTypeComboBox.ItemsSource = coverTypes;
            CoverTypeComboBox.DisplayMemberPath = "Name";
            CoverTypeComboBox.SelectedValuePath = "Id";
        }

        private void LoadDataToDataGrid(ObservableCollection<Product> productList)
        {
            int columnCount = 4;
            int row = 0, column = 0;

            ContainerProduct.RowDefinitions.Clear();
            ContainerProduct.Children.Clear();

            foreach (var product in productList)
            {
                var productUC = new ProductUserControl(_unitOfWork,_userAuthen,_mapper);

                productUC.DataContext = product;
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var destinationDirectory = Directory.GetCurrentDirectory() + "\\images\\product\\";
                    productUC.imgDisplay.Source = new BitmapImage(new Uri(destinationDirectory + product.ImageUrl));
                }

                if (column == 0)
                {
                    ContainerProduct.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = GridLength.Auto
                    });
                }

                Grid.SetRow(productUC, row);
                Grid.SetColumn(productUC, column);
                ContainerProduct.Children.Add(productUC);

                column++;
                if (column >= columnCount)
                {
                    column = 0;
                    row++;
                }
            }
        }

        private void filter_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;

            double.TryParse(PriceMinTextBox.Text, out double priceMin);
            double.TryParse(PriceMaxTextBox.Text, out double priceMax);

            int? selectedCategoryId = CategoryComboBox.SelectedValue as int?;
            int? selectedCoverTypeId = CoverTypeComboBox.SelectedValue as int?;

            var filteredProducts = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category,CoverType");

            if (!string.IsNullOrWhiteSpace(title))
                filteredProducts = filteredProducts.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(author))
                filteredProducts = filteredProducts.Where(p => p.Author.Contains(author, StringComparison.OrdinalIgnoreCase));

            if (priceMin > 0)
                filteredProducts = filteredProducts.Where(p => p.Price >= priceMin);

            if (priceMax > 0)
                filteredProducts = filteredProducts.Where(p => p.Price <= priceMax);

            if (selectedCategoryId.HasValue && selectedCategoryId.Value!=0)
                filteredProducts = filteredProducts.Where(p => p.CategoryId == selectedCategoryId.Value);

            if (selectedCoverTypeId.HasValue && selectedCoverTypeId.Value!=0)
                filteredProducts = filteredProducts.Where(p => p.CoverTypeId == selectedCoverTypeId.Value);
            LoadDataToDataGrid(new ObservableCollection<Product>(filteredProducts));
        }
    }
}
