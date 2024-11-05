using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Dialogs;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
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

namespace BulkyBook.Pages
{
    /// <summary>
    /// Interaction logic for CategoryManagementPage.xaml
    /// </summary>
    public partial class CategoryManagementPage : Page
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ObservableCollection<CategoryVM> Categories { get; set; }

        public CategoryManagementPage(IUnitOfWork unitOfWork, IMapper mapper)
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
            var dialog = new CategoryDialog();
            var window = new Window
            {
                Title = "Create Category",
                Content = dialog,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            dialog.Load();
            if (window.ShowDialog() == true)
            {
                var category = dialog.Category;
                _unitOfWork.CategoryRepository.Add(_mapper.Map<Category>(category));
                _unitOfWork.Save();
                Load();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvCategory.SelectedItem is CategoryVM selectedCategory)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selectedCategory.Name}?",
                                              "Confirm Delete",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _unitOfWork.CategoryRepository.DeleteCategory(_mapper.Map<Category>(dgvCategory.SelectedItem));
                    _unitOfWork.Save();
                    Load();
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private  void Load()
        {
            var categories = _mapper.Map<IEnumerable<CategoryVM>>( _unitOfWork.CategoryRepository.GetAll().OrderBy(c => c.DisplayOrder).ToList());
            Categories = new ObservableCollection<CategoryVM>(categories);
            dgvCategory.ItemsSource = Categories;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void dgvCategory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvCategory.SelectedItem is CategoryVM selectedCategory)
            {
                var dialog = new CategoryDialog();
                dialog.Category = selectedCategory;
                var window = new Window
                {
                    Title = "Update Category",
                    Content = dialog,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                dialog.Load();
                if (window.ShowDialog() == true)
                {
                    selectedCategory.Name = dialog.Category.Name;
                    selectedCategory.DisplayOrder = dialog.Category.DisplayOrder;
                    _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(selectedCategory));
                    _unitOfWork.Save();
                    Load();
                }
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                bool isNumericSearch = int.TryParse(searchText, out int searchId);

                var filteredCategories = Categories
                    .Where(c =>
                        (c.Name != null && c.Name.ToLower().Contains(searchText)) ||
                        (isNumericSearch && c.Id == searchId)
                    )
                    .ToList();
                dgvCategory.ItemsSource = new ObservableCollection<CategoryVM>(filteredCategories);
            }
            else
            {
                dgvCategory.ItemsSource = Categories;
            }
        }
    }
}
