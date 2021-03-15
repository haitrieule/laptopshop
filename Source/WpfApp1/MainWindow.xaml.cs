using Aspose.Cells;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class PagingInfo
        {
            public int RowsPerPage { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
            public int TotalItems { get; set; }
            public List<string> Pages
            {
                get
                {
                    var result = new List<string>();

                    for (var i = 1; i <= TotalPages; i++)
                    {
                        result.Add($"Trang {i} / {TotalPages}");
                    }

                    return result;
                }
            }
        }

        PagingInfo _pagingInfo;
        int rowsPerPage = 4;
        //int _selectedCategoryIndex = 0;
        private void BackstageTabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }


        //class Category
        //{
        //    public int Category_Id { get; set; }
        //    public string Category_Name { get; set; }
        //    public BindingList<Product> Products { get; set; }
        //}

        //class Product
        //{
        //    public int Product_Id { get; set; }
        //    public int Category_Id { get; set; }
        //    public string Product_Name { get; set; }
        //    public Category Category { get; set; }
        //}

        ////ViewModel
        //BindingList<Category> categories { get; set; }
        //BindingList<Product> products { get; set; }

        // Usercontrol: Initialized

        private async void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //statusLabel.Content = "Application is ready";

            //// Keets nooi CSDL lay dl len
            //categories = new BindingList<Category>()
            //{
            //    new Category()
            //    {
            //        Category_Id = 1, Category_Name = "HP",
            //        Products = new BindingList<Product>()
            //        {
            //            new Product()
            //            {
            //                Product_Id = 1, 
            //                Product_Name = "HP Probook"
            //            },
            //            new Product()
            //            {
            //                Product_Id = 2,
            //                Product_Name = "HP Omen"
            //            },
            //            new Product()
            //            {
            //                Product_Id = 3,
            //                Product_Name = "HP Envy"
            //            }
            //        }
            //    },
            //    new Category()
            //    {
            //         Category_Id = 2, Category_Name = "Dell",
            //         Products = new BindingList<Product>()
            //         {
            //             new Product()
            //             {
            //                Product_Id = 4,
            //                Product_Name = "Dell Alienware"
            //             },
            //             new Product()
            //             {
            //                Product_Id = 5,
            //                Product_Name = "Dell Vostro"
            //             }
            //         }
            //    }
            //};
            loadingProgressBar.IsIndeterminate = true;

            // Ket noi CSDL
            // Gan ds categories vao trong combobox
            // Chon phan tu dau tien
            // Su kien selection change cua  combobox
            //      Nap ds product tuong ung



            await Task.Run(() => {
                System.Threading.Thread.Sleep(2000);
            });

            var db = new MyStoreEntities3();
            


            var categories = db.Categories.ToArray();
            categoriesComboBox.ItemsSource = categories;
            categoriesComboBox.SelectedIndex = 0;

            statusTextBlock.Text = "All products is loaded";
            loadingProgressBar.IsIndeterminate = false;

            var allPurchaseStatus = db.PurchaseStatusEnums.ToList();
            //MessageBox.Show(allPurchaseStatus.ToString());
            purchaseStatesComboBox.ItemsSource = allPurchaseStatus;
            purchaseStatesComboBox.SelectedIndex = 0;
            

            loadAllPurchases();
        }
 
        void loadAllPurchases()
        {
            int status = (purchaseStatesComboBox.SelectedItem as PurchaseStatusEnum).Value;
            MyStoreEntities3 db = new MyStoreEntities3();
            IQueryable<Purchase> dateFilter;
            if (fromDatePicker.SelectedDate != null && toDatePicker.SelectedDate != null)
            {
                var from = (DateTime)fromDatePicker.SelectedDate;
                var to = (DateTime)toDatePicker.SelectedDate;
                dateFilter = db.Purchases.Where(p => (from <= p.Created_At) && (p.Created_At <= to));
            }
            else {
                dateFilter = db.Purchases;
            }
            
            var query = dateFilter.GroupJoin(db.Customers,
               p => p.Customer_Tel,
               c => c.Tel,
               (x, y) => new { Purchases = x, Customers = y }
               )
               .SelectMany(
                    x => x.Customers.DefaultIfEmpty(),
                    (x, y) => new { Purchase = x.Purchases, Customer = y }
                )
                .Select(item => new
                {
                    item.Purchase.Status,
                    item.Purchase.Purchase_ID,
                    item.Purchase.Total,
                    item.Purchase.Created_At,
                    item.Customer.Customer_Name,
                    item.Customer.Tel
                }).OrderByDescending(p=>p.Created_At).Join(db.PurchaseStatusEnums,item=> item.Status, s=>s.Value, (item, s)=> new {
                    item.Status,
                    item.Purchase_ID,
                    item.Total,
                    item.Created_At,
                    item.Customer_Name,
                    item.Tel,
                    s.Description
                });

            if (status != -1)
            {
                //MessageBox.Show("aaa");
                var subquery = query.Where(p => p.Status == status);
                purchaseDataGrid.ItemsSource = subquery.ToList();
            }
            else {
                purchaseDataGrid.ItemsSource = query.ToList();
            }



            //    .SelectMany(
            //        x => x.Customers.DefaultIfEmpty(),
            //        (x, y) => new {
            //            x.Purchase_ID,
            //            x.Total,
            //            p.Created_At,
            //            Details = p.PurchaseDetails.Join(db.Products,
            //            p1 => p1.Product_ID,
            //            product => product.Product_ID,
            //            (p1, product) => new
            //            {
            //                Product_Name = product.Product_Name,
            //                Unit_Price = p1.Price,
            //                Quantity = p1.Quantity,
            //                Sub_Total = p1.Total
            //            }
            //        ),
            //        });



            // (p, groups) => new {

            //        Groups = groups
            //    }
            //)
            //     .SelectMany()
            //     .ToList();

            // var subquery = query.Select(q => new
            // {
            //     Purchase_ID = q.Purchase_ID,
            //     Customer_Name = q.Customer_Name,
            //     Tel = q.Tel,
            //     Total = q.Total,
            //     Created_At = q.Created_At,
            //     Details = q.Details.Select((item, index) => new
            //     {
            //         STT = index + 1,
            //         Product_Name = item.Product_Name,
            //         Unit_Price = item.Unit_Price,
            //         Quantity = item.Quantity,
            //         Sub_Total = item.Sub_Total
            //     })
            // }).ToList();

        }//GOI Y

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int index = categoriesComboBox.SelectedIndex;
            //if (index >= 0)
            //{
            //    // Nap ds cac product va gan vao listview
            //    var products = categories[index].Products;
            //    productsListView.ItemsSource = products;
            //}
            //Debug.WriteLine($"Index changed: {index}");

            loadingProgressBar.IsIndeterminate = true;
            await Task.Run(() => {
                System.Threading.Thread.Sleep(1000);
            });

            CalculatePagingInfo();
            UpdateProductView();

            //var db = new MyStoreEntities2();
            //var selectedCategory = categoriesComboBox.SelectedItem
            //    as  Category;
            //var products =
            //    db.Categories.Find(selectedCategory.Id)
            //        .Products;
            //var query = from product in products
            //            select new
            //            {
            //                product_name= product.Name,
            //                Thumbnail = product.Photos
            //                    .First().Data
            //            };
            //productsListView.ItemsSource = query;
            loadingProgressBar.IsIndeterminate = false;
        }

        private void importexcel_Click(object sender, RoutedEventArgs e)// da xong
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var fileinfo = new FileInfo(filename);

                var excelFile = new Workbook(filename);
                var tabs = excelFile.Worksheets;

                var db = new MyStoreEntities3();
                foreach (var tab in tabs)
                {
                    var category = new Category()
                    {
                        Name = tab.Name
                    };
                    db.Categories.Add(category);
                    db.SaveChanges();

                    var row = 3;

                    var cell = tab.Cells[$"C3"];
                    while (cell.Value != null)
                    {
                        var product = new Product()
                        {
                            SKU = tab.Cells[$"C{row}"].StringValue,
                            Name = tab.Cells[$"D{row}"].StringValue,
                            Price = tab.Cells[$"E{row}"].IntValue,
                            Quantity = tab.Cells[$"F{row}"].IntValue,
                            Description = tab.Cells[$"G{row}"].StringValue,
                            Image = tab.Cells[$"H{row}"].StringValue
                        };

                        category.Products.Add(product);
                        db.SaveChanges();

                        var imageName = tab.Cells[$"H{row}"].StringValue;
                        var imageFull = $"{fileinfo.Directory}\\images\\{imageName}";
                        var image = new BitmapImage(new Uri(imageFull, UriKind.Absolute));
                        var encoder = new JpegBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(image));

                        using (var stream = new MemoryStream())
                        {
                            encoder.Save(stream);
                            var photo = new Photo()
                            {
                                Product_id = product.Id,
                                Data = stream.ToArray()
                            };
                            db.Photos.Add(photo);

                            db.SaveChanges();
                        }


                        // Chuyển thành mảng byte và lưu vào CSDl




                        row++; //Di qua dong ke
                        cell = tab.Cells[$"C{row}"];
                    }

                    MessageBox.Show("Data imported");
                }
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            var currentIndex = pagingComboBox.SelectedIndex;
            if (currentIndex > 0)
            {
                pagingComboBox.SelectedIndex--;
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            var currentIndex = pagingComboBox.SelectedIndex;
            if (currentIndex >= 0)
            {
                pagingComboBox.SelectedIndex++;
            }
        }
        void CalculatePagingInfo()
        {
            // Ket noi CSDL va lay dl tuong ung
            //var _selectedCategoryIndex = categoriesComboBox.SelectedIndex;
            //var products = _categories[_selectedCategoryIndex].Products;

            //var keyword = keywordTextBox.Text;
            //var query = from product in products
            //            where product.Name.ToLower()
            //                    .Contains(keyword.ToLower())
            //            select product;

            var db = new MyStoreEntities3();
            var selectedCategory = categoriesComboBox.SelectedItem
                as Category;
            var products =
                db.Categories.Find(selectedCategory.Id)
                    .Products;
            var query = from product in products
                        select new
                        {
                            product_name = product.Name,
                            Thumbnail = product.Photos
                                .First().Data,

                        };
            // Tinh toan thong tin phan trang
            var count = query.Count();
            _pagingInfo = new PagingInfo()
            {
                RowsPerPage = rowsPerPage,
                TotalItems = count,
                TotalPages = count / rowsPerPage +
                    (((count % rowsPerPage) == 0) ? 0 : 1),
                CurrentPage = 1
            };

            pagingComboBox.ItemsSource = _pagingInfo.Pages;
            pagingComboBox.SelectedIndex = 0;

            statusTextBlock.Text = $"Tổng sản phẩm tìm thấy: {count} ";
        }
        void UpdateProductView()
        {
            //var _selectedCategoryIndex = categoriesComboBox.SelectedIndex;
            //var products = _categories[_selectedCategoryIndex].Products;
            //var keyword = keywordTextBox.Text;
            //var query = from product in products
            //            where product.Name.ToLower().Contains(keyword.ToLower())
            //            select product;


            var db = new MyStoreEntities3();
            var selectedCategory = categoriesComboBox.SelectedItem
                as Category;
            var products =
                db.Categories.Find(selectedCategory.Id)
                    .Products;
            var keyword = keywordTextBox.Text;
            //var query = from product in products
            //            select new
            //            {
            //                product_name = product.Name,
            //                Thumbnail = product.Photos
            //                    .First().Data
            //            };
            var query = from product in products
                        where product.Name.ToLower().Contains(keyword.ToLower())
                        select new
                        {
                            product_name = product.Name,
                            Thumbnail = product.Photos
                                .First().Data
                        };

            // Gan du lieu cho list view de o cuoi cung
            // Dua theo trang hien tai
            var skip = (_pagingInfo.CurrentPage - 1) * _pagingInfo.RowsPerPage;
            var take = _pagingInfo.RowsPerPage;
            productsListView.ItemsSource = query.Skip(skip).Take(take);

            productsListView.SelectedIndex = 0;
        }

        private void keywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculatePagingInfo();
            UpdateProductView();
        }

        private void pagingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int nextPage = pagingComboBox.SelectedIndex + 1;
            _pagingInfo.CurrentPage = nextPage;

            UpdateProductView();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            //var category = productsListView.SelectedItem as Category;
            //var screen = new EditProctWindow(Category)
            var category = categoriesComboBox.SelectedItem as Category;
            //MessageBox.Show(categoriesComboBox.SelectedItem.ToString());
            var screen = new EditCategoryWindow(category);
            if (screen.ShowDialog() == true)
            {
                var add = screen.Category;
                var db = new MyStoreEntities3();
                db.Categories.Add(add);
                db.SaveChanges();

                CalculatePagingInfo();
                UpdateProductView();
                var categories = db.Categories.ToArray();
                categoriesComboBox.ItemsSource = categories;
                categoriesComboBox.SelectedIndex = 0;

            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = categoriesComboBox.SelectedItem as Category;

            var db = new MyStoreEntities3();

            var a = db.Categories.Where(c => c.Id == category.Id).Single();
            db.Categories.Remove(a);
            db.SaveChanges();

            var categories = db.Categories.ToArray();
            categoriesComboBox.ItemsSource = categories;
            categoriesComboBox.SelectedIndex = 0;

        }

        private void EditCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = categoriesComboBox.SelectedItem as Category;
            var screen = new EditCategoryWindow(category);
            if (screen.ShowDialog() == true)
            {
                var add = screen.Category;
                var db = new MyStoreEntities3();
                var a = db.Categories.Where(c => c.Id == category.Id).Single();
                a.Name = add.Name;
                db.SaveChanges();

                CalculatePagingInfo();
                UpdateProductView();
                var categories = db.Categories.ToArray();
                categoriesComboBox.ItemsSource = categories;
                categoriesComboBox.SelectedIndex = 0;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        int a;
        int b;
        private void savechanged(object sender, SelectionChangedEventArgs e)
        {
            a = productsListView.SelectedIndex;
            //MessageBox.Show(a.ToString());
            if (a != -1) {
                b = a;
            }

            //.Show(b.ToString());
        }

        private void EditProducte_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.Close();
                var db = new MyStoreEntities3();
                var selectedCategory = categoriesComboBox.SelectedItem
                    as Category;
                var products =
                    db.Categories.Find(selectedCategory.Id)
                        .Products;
                var keyword = keywordTextBox.Text;
                var query1 = from producta in products
                             where producta.Name.ToLower().Contains(keyword.ToLower())
                             select producta;
                var skip = (_pagingInfo.CurrentPage - 1) * _pagingInfo.RowsPerPage;
                var take = _pagingInfo.RowsPerPage;
                productsListView.ItemsSource = query1.Skip(skip).Take(take);
                productsListView.SelectedIndex = b;
                var product = productsListView.SelectedItem as Product;

                var screen = new AddEditProductWindow(product);
                if (screen.ShowDialog() == true)
                {
                    var add = screen.Product;
                    db = new MyStoreEntities3();
                    var a = db.Products.Where(c => c.Id == product.Id).Single();
                    a.Name = add.Name;
                    db.SaveChanges();


                    CalculatePagingInfo();
                    UpdateProductView();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.Close();
                var db = new MyStoreEntities3();
                var selectedCategory = categoriesComboBox.SelectedItem
                    as Category;
                var products =
                    db.Categories.Find(selectedCategory.Id)
                        .Products;
                var keyword = keywordTextBox.Text;
                var query1 = from producta in products
                             where producta.Name.ToLower().Contains(keyword.ToLower())
                             select producta;
                var skip = (_pagingInfo.CurrentPage - 1) * _pagingInfo.RowsPerPage;
                var take = _pagingInfo.RowsPerPage;
                productsListView.ItemsSource = query1.Skip(skip).Take(take);
                productsListView.SelectedIndex = b;
                var product = productsListView.SelectedItem as Product;

                var a = db.Products.Where(c => c.Id == product.Id).Single();
                db.Products.Remove(a);
                db.SaveChanges();

                CalculatePagingInfo();
                UpdateProductView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void addPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddPurchaseWindow();
            if (screen.ShowDialog() == true)
            {
                loadAllPurchases();
            }
            MessageBox.Show("thêm dữ liệu thành công");
        }

        private void editPurchase_MenuItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var db = new MyStoreEntities3();
                dynamic item = purchaseDataGrid.SelectedItem;
                var purchase = db.Purchases.Find(item.Purchase_ID);
                db.Purchases.Remove(purchase);
                db.SaveChanges();

                // Reload giao dien
                loadAllPurchases();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            var screen = new AddPurchaseWindow();
            if (screen.ShowDialog() == true)
            {
                loadAllPurchases();
            }
            MessageBox.Show("sửa dữ liệu thành công");
        }

        private void deletePurchase_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try {
                var db = new MyStoreEntities3();
                dynamic item = purchaseDataGrid.SelectedItem;
                var purchase = db.Purchases.Find(item.Purchase_ID);
                db.Purchases.Remove(purchase);
                db.SaveChanges();

                // Reload giao dien
                loadAllPurchases();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            // Thong bao phan hoi
            MessageBox.Show("Đơn hàng đã được xóa thành công");
        }



        private void purchaseStatesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var item = purchaseStatesComboBox.SelectedItem as PurchaseStatusEnum;
            loadAllPurchases();
        }

        private void fromDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void toDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fromDatePicker.SelectedDate != null)
            {
                var from = (DateTime)fromDatePicker.SelectedDate;
                var to = (DateTime)toDatePicker.SelectedDate;

                if (DateTime.Compare(from, to) < 0)
                {
                    loadAllPurchases();
                }
                else
                {
                    MessageBox.Show("Ngày kết thúc phải lớn hơn ngyaf bát đầu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ConHang_Click(object sender, RoutedEventArgs e)
        {
            var db = new MyStoreEntities3();
            var query = from a in db.Products join b in db.PurchaseDetails on a.Id equals b.Product_ID where a.Quantity > b.Quantity select new {name = a.Name, SLCL = a.Quantity-b.Quantity };

            purchaseDataGrid1.ItemsSource = query.ToList();
        }

        private void SapHetHang_Click(object sender, RoutedEventArgs e)
        {
            var db = new MyStoreEntities3();
            var query = from a in db.Products join b in db.PurchaseDetails on a.Id equals b.Product_ID where a.Quantity - b.Quantity<5 && a.Quantity - b.Quantity > 0 select new { name = a.Name, SLCL = a.Quantity - b.Quantity };

            purchaseDataGrid1.ItemsSource = query.ToList();

        }

        private void DonHangGanNhat_Click(object sender, RoutedEventArgs e)
        {
            var screen = new ShowPurchaseWindow();
            screen.ShowDialog();
        }

        private void DoanhThu_Click(object sender, RoutedEventArgs e)
        {
            var screen = new doanhthu();
            screen.ShowDialog();
        }

        private void BanChay_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
