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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : Window
    {
        public Product Product; 
        Product _producta;
        Product _product;
        Product _product2;
        Product _product3;
        Product _product4;
        Product _product5;
        Product _productanh;
        public AddEditProductWindow(Product product)
        {
            InitializeComponent();
            
            try
            {
                _producta = new Product()
                {
                   CatId = product.CatId
                };
                _product = new Product()
                {
                    SKU = product.SKU

                };
                _product2 = new Product()
                {
                    Name = product.Name
                };
                _product3 = new Product()
                { 
                    Price = product.Price
                };
                _product4 = new Product()
                { 
                    Quantity= product.Quantity
                };
                _product5 = new Product()
                {
                    Description = product.Description
                };
                _productanh = new Product()
                {
                    Image = product.Image
                };
                
                idTextbox.DataContext = _producta;
                skuTextbox.DataContext = _product;
                nameTextbox.DataContext = _product2;
                priceTextbox.DataContext = _product3;
                quantityTextbox.DataContext = _product4;
                DescriptionTextbox.DataContext = _product5;
                imageTextbox.DataContext = _productanh;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Product = new Product()
            {
                CatId= _producta.CatId,
                SKU = _product.SKU,
                Name = _product2.Name,
                Price=_product3.Price,
                Quantity = _product4.Quantity,
                Description = _product5.Description,
                Image = _productanh.Image
                

            };
            DialogResult = true;
        }
    }
}
