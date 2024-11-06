using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.UserControls;
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
    /// Interaction logic for UserCategoriesPage.xaml
    /// </summary>
    public partial class UserCategoriesPage : Page
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserVM _userAuthen;
        private readonly IMapper _mapper;

        public UserCategoriesPage(IUnitOfWork unitOfWork, ApplicationUserVM userAuthen, IMapper mapper)
        {
            _mapper = mapper;
            _userAuthen = userAuthen;
            _unitOfWork = unitOfWork;
            InitializeComponent();
        }

        private void Page_Loaed(object sender, RoutedEventArgs e)
        {
            LoadCategoriesList();
        }
        public void LoadCategoriesList()
        {
            var categories = new ObservableCollection<CategoryVM>(
                _mapper.Map<IEnumerable<CategoryVM>>(_unitOfWork.CategoryRepository.GetAll()));
            dgUserCategories.ItemsSource = categories;
        }

        private void dgUserCategories_SelectChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgUserCategories.SelectedItem is CategoryVM selectedCategory)
            {
                int selectedCategoryId = selectedCategory.Id;
                var productList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category,CoverType").Where(p => p.CategoryId==selectedCategoryId);
                LoadDataToDataGrid(new ObservableCollection<Product>(productList.Take(16)));

            }
        }

        private void LoadDataToDataGrid(ObservableCollection<Product> productList)
        {
            int columnCount = 3;
            int row = 0, column = 0;

            ContainerProduct.RowDefinitions.Clear();
            ContainerProduct.Children.Clear();

            foreach (var product in productList)
            {
                var productUC = new ProductUserControl(_unitOfWork, _userAuthen, _mapper);

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
    }
}
