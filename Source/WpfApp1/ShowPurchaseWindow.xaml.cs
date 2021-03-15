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
    /// Interaction logic for ShowPurchaseWindow.xaml
    /// </summary>
    public partial class ShowPurchaseWindow : Window
    {
        public ShowPurchaseWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MyStoreEntities3 db = new MyStoreEntities3();
            //var query = from a in db.Products join b in db.PurchaseDetails on a.Id equals b.Product_ID where a.Quantity > b.Quantity select new { name = a.Name, SLCL = a.Quantity - b.Quantity };

            var query = (from a in db.Purchases select new { tel = a.Customer_Tel, Created_At = a.Created_At, Total = a.Total, Description = a.Status}).OrderByDescending(a=>a.Created_At).Take(3);
            purchaseDataGrid2.ItemsSource = query.ToList();
        }
    }
}
