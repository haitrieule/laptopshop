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
using LiveCharts;
using LiveCharts.Wpf;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for doanhthu.xaml
    /// </summary>
    public partial class doanhthu : Window
    {
        public doanhthu()
        {
            InitializeComponent();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyStoreEntities3 db = new MyStoreEntities3();
                List<int?> values = new List<int?>();
                var query = from a in db.Purchases select new { a.Total };
                var b = query.SingleOrDefault().Total;
                values.Add(b);
                var series = new SeriesCollection()
            {
                new LineSeries()
                {
                    Values = new ChartValues<int?> (values)


                }
            };
                reportChart.Series = series;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
    //MyStoreEntities3 db = new MyStoreEntities3();
}
    }
}
