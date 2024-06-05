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

    namespace VTET.WpfApp.UI
    {
        /// <summary>
        /// Interaction logic for wOrderDetail.xaml
        /// </summary>
        public partial class wOrderDetail : Window
        {
            public readonly IOrderDetailBusiness _orderdetailbusiness;

            public wOrderDetail()
            {
                InitializeComponent();
                this._orderdetailbusiness ??= new OrderDetailBusiness();
                this.LoadGrdOrderDetail();
            }


            private async void LoadGrdOrderDetail()
            {
                var result = await _orderdetailbusiness.GetAll();

                if (result.Status > 0 && result.Data != null)
                {
                    grdOrderDetail.ItemsSource = result.Data as List<OrderDetail>;
                }
                else
                {
                    grdOrderDetail.ItemsSource = new List<OrderDetail>();
                }
            }


            private async void ButtonSave_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    int orderDetailIdTmp = -1;
                    int.TryParse(txtOrderDetailId.Text, out orderDetailIdTmp);
                    var item = await _orderdetailbusiness.GetById(orderDetailIdTmp);
                    if (item.Data == null)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Id = orderDetailIdTmp,
                            OrderId = int.Parse(txtOrderId.Text),
                            WatchId = int.Parse(txtWatchId.Text),
                            Price = int.Parse(txtPrice.Text),
                            Amount = int.Parse(txtAmount.Text)
                        };

                        var result = await _orderdetailbusiness.Save(orderDetail);
                        MessageBox.Show(result.Message, "Save");
                    }
                    else
                    {
                        var orderDetail = item.Data as OrderDetail;
                        orderDetail.OrderId = int.Parse(txtOrderId.Text);
                        orderDetail.WatchId = int.Parse(txtWatchId.Text);
                        orderDetail.Price = int.Parse(txtPrice.Text);
                        orderDetail.Amount = int.Parse(txtAmount.Text);

                        var result = await _orderdetailbusiness.Update(orderDetail);
                        MessageBox.Show(result.Message, "Update");
                    }

                    txtOrderDetailId.Text = string.Empty;
                    txtOrderId.Text = string.Empty;
                    txtWatchId.Text = string.Empty;
                    txtPrice.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    this.LoadGrdOrderDetail();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }

            }
            private async void grdOrderDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
            {
                if (sender is Button button && button.CommandParameter is int id)
                {
                    var result = await _orderdetailbusiness.Delete(id);
                    if (result.Status > 0)
                    {
                        MessageBox.Show(result.Message, "Delete");
                        LoadGrdOrderDetail();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Error");
                    }
                }
            }

            private void ButtonCancel_Click(object sender, RoutedEventArgs e)
            {
            txtOrderDetailId.Text = string.Empty;
            txtOrderId.Text = string.Empty;
            txtWatchId.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtAmount.Text = string.Empty;
            this.LoadGrdOrderDetail();
            }








            private async Task grdOrderDetail_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
            {
                //MessageBox.Show("Double Click on Grid");
                DataGrid grd = sender as DataGrid;
                if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
                {
                    var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                    if (row != null)
                    {
                        var item = row.Item as OrderDetail;
                        var orderDetailResult = await _orderdetailbusiness.GetById(item.Id);

                        if (orderDetailResult.Status > 0 && orderDetailResult.Data != null)
                        {
                            var orderDetail = orderDetailResult.Data as OrderDetail;
                            if (orderDetail != null)
                            {
                                txtOrderDetailId.Text = orderDetail.Id.ToString();
                                txtWatchId.Text = orderDetail.WatchId.ToString();
                                txtOrderId.Text = orderDetail.OrderId.ToString();
                                txtPrice.Text = orderDetail.Price.ToString();
                                txtAmount.Text = orderDetail.Amount.ToString();
                            }
                        }

                    }
                }
            }

        private async void grdOrderDetail_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
                //MessageBox.Show("Double Click on Grid");
                DataGrid grd = sender as DataGrid;
                if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
                {
                    var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                    if (row != null)
                    {
                        var item = row.Item as OrderDetail;
                        var orderDetailResult = await _orderdetailbusiness.GetById(item.Id);

                        if (orderDetailResult.Status > 0 && orderDetailResult.Data != null)
                        {
                            var orderDetail = orderDetailResult.Data as OrderDetail;
                            if (orderDetail != null)
                            {
                                txtOrderDetailId.Text = orderDetail.Id.ToString();
                                txtWatchId.Text = orderDetail.WatchId.ToString();
                                txtOrderId.Text = orderDetail.OrderId.ToString();
                                txtPrice.Text = orderDetail.Price.ToString();
                                txtAmount.Text = orderDetail.Amount.ToString();
                            }
                        }

                    }
                }
            }
        }
    }
