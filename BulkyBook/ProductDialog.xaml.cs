using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BulkyBook
{
    /// <summary>
    /// Interaction logic for ProductDialog.xaml
    /// </summary>
    public partial class ProductDialog : UserControl
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductVM Product { get; set; }

        public ProductDialog(IUnitOfWork unitOfWork)
        {
            Product = new ProductVM();
            _unitOfWork = unitOfWork;
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductTitle.Text))
            {
                MessageBox.Show("Product Title is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtProductTitle.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("Product ISBN is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtISBN.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Product Author is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtAuthor.Focus();
                return;
            }

            if (!double.TryParse(txtPrice.Text, out _))
            {
                MessageBox.Show("Product Price must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPrice.Focus();
                return;
            }

            Product.Product.Price = Convert.ToDouble(txtPrice.Text);
            Product.Product.CategoryId = Product.Product.Category.Id;
            Product.Product.CoverTypeId = Product.Product.CoverType.Id;

            (this.Parent as Window).DialogResult = true;
            (this.Parent as Window)?.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Window)?.Close();
        }

        public void Load()
        {
            var categoryList = _unitOfWork.CategoryRepository.GetAll();
            var coverTypeList = _unitOfWork.CoverTypeRepository.GetAll();

            this.Product.CategoryList = categoryList;
            this.Product.CoverTypeList = coverTypeList;

            DataContext = Product;

            if (!string.IsNullOrEmpty(Product.Product.ImageUrl))
            {
                var destinationDirectory = Directory.GetCurrentDirectory() + "\\images\\product\\";
                imgDisplay.Source = new BitmapImage(new Uri(destinationDirectory + Product.Product.ImageUrl));
            }
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                var extension = System.IO.Path.GetExtension(filePath);

                var fileName = Guid.NewGuid().ToString() + extension;

                var destinationDirectory = Directory.GetCurrentDirectory() + "\\images\\product\\";
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                File.Copy(openFileDialog.FileName, destinationDirectory + fileName);

                // lỗi ở đây vì ảnh đang hiển thị ở form
                // khi thay đổi ảnh, ảnh cũ vẫn còn trong form nên bị lock
                // cần xóa ảnh cũ đi trước khi hiển thị ảnh mới
                //if (!string.IsNullOrEmpty(Product.Product.ImageUrl))
                //{
                //    var oldFilePath = destinationDirectory + Product.Product.ImageUrl;
                //    if (File.Exists(oldFilePath))
                //    {
                //        imgDisplay.Source = null;
                //        GC.Collect();
                //        GC.WaitForPendingFinalizers();

                //        File.Delete(oldFilePath);
                //    }
                //}

                imgDisplay.Source = new BitmapImage(new Uri(destinationDirectory + fileName));
                Product.Product.ImageUrl = fileName;
            }
        }
    }
}
