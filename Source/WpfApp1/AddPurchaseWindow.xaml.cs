﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for AddPurchaseWindow.xaml
    /// </summary>
    public partial class AddPurchaseWindow : Window
    {
        public AddPurchaseWindow()
        {
            InitializeComponent();
            var query = db.Products.ToList();
            productsListView.ItemsSource = query.ToList();

        }
        BindingList<object> list = new BindingList<object>();

        private void addPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string customer_tel = customerTelTextBox.Text.Length == 0 ? null : customerTelTextBox.Text;
                if (customer == null && customerTelTextBox.Text.Length != 0) {
                    var newCustomer = new Customer()
                    {
                        Tel = customerTelTextBox.Text,
                        Customer_Name = customerNameTextBox.Text
                    };
                    db.Customers.Add(newCustomer);
                    db.SaveChanges();
                }
                {

                }
                var purchase = new Purchase()
                {
                    Created_At = DateTime.Now,
                    Total = list.Sum((dynamic p) => p.SubTotal),

                    Status=1,
                    Customer_Tel=customer_tel
                };

                foreach (dynamic item in list)
                {
                    purchase.PurchaseDetails.Add(new PurchaseDetail()
                    {
                        Product_ID = item.Product_ID,
                        Price = item.Unit_Price,
                        Quantity = item.Quantity,
                        Total = item.SubTotal
                    });
                }
                db.Purchases.Add(purchase);
                db.SaveChanges();


                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        MyStoreEntities3 db = new MyStoreEntities3();
        private void productsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            var item = productsListView.SelectedItem as Product;

            // Kiểm tra sản phẩm đã có sẵn hay chưa
            var foundIndex = -1;
            for (int i = 0; i < list.Count; i++)
            {
                dynamic p = list[i];
                if (p.Product_ID == item.Id)
                {
                    var updatedProduct = new
                    {
                        Product_ID = item.Id,
                        Product_Name = item.Name,
                        SubTotal = (p.Quantity + 1) * p.Unit_Price,
                        Quantity = p.Quantity + 1,
                        Unit_Price = p.Unit_Price
                    };
                    list.RemoveAt(i);
                    list.Insert(i, updatedProduct);

                    foundIndex = i; // báo hiệu đã tìm thấy
                }
            }

            if (foundIndex == -1) // Chưa cập nhật
            {
                list.Add(new
                {
                    Product_ID = item.Id,
                    Product_Name = item.Name,
                    Quantity = 1,
                    Unit_Price = item.Price,
                    SubTotal = item.Price
                });
            }

            int sum = 0;
            foreach (dynamic product in list)
            {
                sum += product.SubTotal;
            }
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string sumText = sum.ToString("#.###", cul.NumberFormat);
            totalTextBlock.Text = $"{sumText} đ";


            selectedProductsListView.ItemsSource = list;
        }
        Customer customer = null;
        private void telTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tel = customerTelTextBox.Text;
            var db = new MyStoreEntities3();
            customer = db.Customers.Find(tel);
            if (customer != null)
            {
                customerNameTextBox.Text = customer.Customer_Name;
            }
        }
    }
}
