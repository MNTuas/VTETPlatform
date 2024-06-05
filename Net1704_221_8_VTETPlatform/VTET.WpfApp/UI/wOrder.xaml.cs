using Microsoft.Identity.Client.NativeInterop;
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
using VTET.Business;
using VTET.Data.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VTET.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wOrder.xaml
    /// </summary>
    public partial class wOrder : Window
    {
        public readonly IOrderBusiness _orderbusiness;

        public wOrder()
        {
            InitializeComponent();
            this._orderbusiness ??= new OrderBusiness();
            this.LoadGrdOrder();
        }

        private async void LoadGrdOrder()
        {
            var result = await _orderbusiness.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdOrder.ItemsSource = result.Data as List<Order>;

                if (grdOrder.Items.Count > 0)
                {
                    var selectedOrder = grdOrder.SelectedItem as Order;
                    if (selectedOrder != null)
                    {
                        Customer_ID.Text = selectedOrder.CustomerId.ToString();
                    }
                }
            }
            else
            {
                grdOrder.ItemsSource = new List<Order>();
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int orderIdTmp = -1;
                int.TryParse(OrderId.Text, out orderIdTmp);
                var item = await _orderbusiness.GetById(orderIdTmp);
                if (item.Data == null)
                {
                    var order = new Order()
                    {
                        //Id = orderId,
                        Email = Email.Text,
                        FullName = FullName.Text,
                        PhoneNumber = PhoneNumber.Text,
                        Date = Date.SelectedDate,
                        TotalPrice = string.IsNullOrEmpty(TotalPrice.Text) ? (decimal?)null : decimal.Parse(TotalPrice.Text),
                        //CustomerId = string.IsNullOrEmpty(Customer_ID.Text) ? (int?)null : int.Parse(Customer_ID.Text)
                    };
                    var result = await _orderbusiness.Save(order);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var order = item.Data as Order;
                    order.Email = FullName.Text;
                    order.FullName = FullName.Text;
                    order.PhoneNumber = PhoneNumber.Text;
                    order.Date = Date.SelectedDate;
                    order.TotalPrice = string.IsNullOrEmpty(TotalPrice.Text) ? (decimal?)null : decimal.Parse(TotalPrice.Text);
                    //order.CustomerId = string.IsNullOrEmpty(Customer_ID.Text) ? (int?)null : int.Parse(Customer_ID.Text);

                    var result = await _orderbusiness.Update(order);
                    MessageBox.Show(result.Message, "Update");
                }
                OrderId.Text = string.Empty;
                Email.Text = string.Empty;
                FullName.Text = string.Empty;
                PhoneNumber.Text = string.Empty;
                Date.SelectedDate = null;
                TotalPrice.Text = string.Empty;
                Customer_ID.Text = string.Empty;

                this.LoadGrdOrder();

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            // Clear các trường nhập liệu
            OrderId.Text = string.Empty;
            Email.Text = string.Empty;
            FullName.Text = string.Empty;
            PhoneNumber.Text = string.Empty;
            Date.SelectedDate = null;
            TotalPrice.Text = string.Empty;
            Customer_ID.Text = string.Empty;
        }

        private async void grdOrder_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button != null)
                {
                    var orderId = button.CommandParameter as int?;
                    if (orderId.HasValue)
                    {
                        var result = await _orderbusiness.Delete(orderId.Value);
                        MessageBox.Show(result.Message, "Delete");
                        this.LoadGrdOrder();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }


        private void grdOrder_MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = grdOrder.SelectedItem as Order;
            if (selectedOrder != null)
            {
                OrderId.Text = selectedOrder.Id.ToString();
                Email.Text = selectedOrder.Email;
                FullName.Text = selectedOrder.FullName;
                Date.SelectedDate = selectedOrder.Date;
                PhoneNumber.Text = selectedOrder.PhoneNumber;
                TotalPrice.Text = selectedOrder.TotalPrice.ToString();
                Customer_ID.Text = selectedOrder.CustomerId.ToString();
            }
        }
    }
}
