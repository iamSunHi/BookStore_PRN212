using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BulkyBook.Dialogs
{
    /// <summary>
    /// Interaction logic for CategoryDialog.xaml
    /// </summary>
    public partial class CategoryDialog : UserControl
    {
        public CategoryVM Category { get; set; }

        public CategoryDialog()
        {
            Category = new CategoryVM();
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtCategoryName.Focus();
                return;
            }

            if (!int.TryParse(txtDisplayOrder.Text, out _))
            {
                MessageBox.Show("Display Order must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtDisplayOrder.Focus();
                return;
            }

            (this.Parent as Window).DialogResult = true;
            (this.Parent as Window)?.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Window)?.Close();
        }
        public void Load()
        {
            DataContext = Category;
        }

        private void txtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }
        private bool IsTextNumeric(string text)
        {
            return int.TryParse(text, out _);
        }
    }
}
