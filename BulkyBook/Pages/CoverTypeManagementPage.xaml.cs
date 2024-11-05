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
    /// Interaction logic for CoverTypeManagementPage.xaml
    /// </summary>
    public partial class CoverTypeManagementPage : Page
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ObservableCollection<CoverTypeVM> CoverTypes { get; set; }

        public CoverTypeManagementPage(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CoverTypeDialog();
            var window = new Window
            {
                Title = "Create CoverType",
                Content = dialog,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            dialog.Load();
            if (window.ShowDialog() == true)
            {
                var coverType = dialog.CoverType;
                _unitOfWork.CoverTypeRepository.Add(_mapper.Map<CoverType>(coverType));
                _unitOfWork.Save();
                Load();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvCoverType.SelectedItem is CoverTypeVM selected)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selected.Name}?",
                                              "Confirm Delete",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _unitOfWork.CoverTypeRepository.Delete(_mapper.Map<CoverType>(dgvCoverType.SelectedItem));
                    _unitOfWork.Save();
                    Load();
                }
            }
            else
            {
                MessageBox.Show("Please select a covertype to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (!string.IsNullOrEmpty(searchText))
            {
                bool isNumericSearch = int.TryParse(searchText, out int searchId);

                var filteredCoverTypes = CoverTypes
                    .Where(c =>
                        (c.Name != null && c.Name.ToLower().Contains(searchText)) ||
                        (isNumericSearch && c.Id == searchId)
                    )
                    .ToList();
                dgvCoverType.ItemsSource = new ObservableCollection<CoverTypeVM>(filteredCoverTypes);
            }
            else
            {
                dgvCoverType.ItemsSource = CoverTypes;
            }
        }
        private void Load()
        {
            var coverTypes = _mapper.Map<IEnumerable<CoverTypeVM>>(_unitOfWork.CoverTypeRepository.GetAll());
            CoverTypes = new ObservableCollection<CoverTypeVM>(coverTypes);
            dgvCoverType.ItemsSource = CoverTypes;
        }
        private void dgvCoverType_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvCoverType.SelectedItem is CoverTypeVM selected)
            {
                var dialog = new CoverTypeDialog();
                dialog.CoverType = selected;
                var window = new Window
                {
                    Title = "Update CoverType",
                    Content = dialog,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                dialog.Load();
                if (window.ShowDialog() == true)
                {
                    selected.Name = dialog.CoverType.Name;
                    _unitOfWork.CoverTypeRepository.Update(_mapper.Map<CoverType>(selected));
                    _unitOfWork.Save();
                    Load();
                }
            }
        }
    }
}
