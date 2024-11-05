using BulkyBook.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace BulkyBook.UserControls
{
    /// <summary>
    /// Interaction logic for RoomCard.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        public ProductUserControl()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ordering is not supported yet!");
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
