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
    /// Interaction logic for EditCategoryWindow.xaml
    /// </summary>
    public partial class EditCategoryWindow : Window
    {
        public Category Category;
        Category _category;
        public EditCategoryWindow(Category category)
        {
            InitializeComponent();
            _category = new Category()
            {
                Name = category.Name
            };
            nameTextBox.DataContext = _category;
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Category = new Category()
            {
                Name = _category.Name
            };
            DialogResult = true;
            
        }
    }
}
