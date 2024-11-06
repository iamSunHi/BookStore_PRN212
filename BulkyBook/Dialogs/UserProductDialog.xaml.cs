using AutoMapper;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using System;
using System.Collections.Generic;
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

namespace BulkyBook.Dialogs
{
    /// <summary>
    /// Interaction logic for UserProductDialog.xaml
    /// </summary>
    public partial class UserProductDialog : UserControl
    {

        private readonly Product _product;
        public UserProductDialog(Product product)
        {
            InitializeComponent();
            _product = product;
            DataContext = _product;
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var destinationDirectory = Directory.GetCurrentDirectory() + "\\images\\product\\";
                imgDisplay.Source = new BitmapImage(new Uri(destinationDirectory + product.ImageUrl));
            }
            //Load_Product();
        }

        private void Load_Product()
        {
            if (_product != null)
            {
                txtProductTitle.Text = _product.Title;
                txtDescription.Text = _product.Description;
                txtISBN.Text = _product.ISBN;
                txtAuthor.Text = _product.Author;
                imgDisplay.Source = new BitmapImage(new Uri(_product.ImageUrl, UriKind.RelativeOrAbsolute));
                txtPrice.Text = _product.Price.ToString();
                txtCategory.Text = _product.Category.Name;
                txtCoverType.Text = _product.CoverType?.Name;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Đóng dialog
            var window = Window.GetWindow(this);
            if (window != null)
            {               
                window.Close(); // Đóng cửa sổ
            }
        }
    }
}
