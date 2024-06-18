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

                decimal price, discount, tax, shippingCost;
                int orderId, watchId, amount;

                if (!int.TryParse(txtOrderId.Text, out orderId))
                {
                    MessageBox.Show("Invalid OrderId", "Error");
                    return;
                }
                if (!int.TryParse(txtWatchId.Text, out watchId))
                {
                    MessageBox.Show("Invalid WatchId", "Error");
                    return;
                }
                if (!decimal.TryParse(txtPrice.Text, out price))
                {
                    MessageBox.Show("Invalid Price", "Error");
                    return;
                }
                if (!int.TryParse(txtAmount.Text, out amount))
                {
                    MessageBox.Show("Invalid Amount", "Error");
                    return;
                }
                if (!decimal.TryParse(txtDiscount.Text, out discount))
                {
                    MessageBox.Show("Invalid Discount", "Error");
                    return;
                }
                if (!decimal.TryParse(txtTax.Text, out tax))
                {
                    MessageBox.Show("Invalid Tax", "Error");
                    return;
                }
                if (!decimal.TryParse(txtShipCost.Text, out shippingCost))
                {
                    MessageBox.Show("Invalid Shipping Cost", "Error");
                    return;
                }

                DateTime? shipmentDate = txtShipDate.SelectedDate;
                DateTime? estimatedDeliveryDate = txtEstimatedDeliveryDate.SelectedDate;

                if (shipmentDate == null)
                {
                    MessageBox.Show("Please select Shipment Date", "Error");
                    return;
                }

                if (estimatedDeliveryDate == null)
                {
                    MessageBox.Show("Please select Estimated Delivery Date", "Error");
                    return;
                }
                int.TryParse(txtOrderDetailId.Text, out orderDetailIdTmp);
                var item = await _orderdetailbusiness.GetById(orderDetailIdTmp);
                if (item.Data == null)
                {
                    var orderDetail = new OrderDetail
                    {
                        Id = orderDetailIdTmp,
                        OrderId = orderId,
                        WatchId = watchId,
                        Price = price,
                        Amount = amount,
                        Discount = discount,
                        Tax = tax,
                        ShippingCost = shippingCost,
                        ShipmentDate = shipmentDate.Value,
                        EstimatedDeliveryDate = estimatedDeliveryDate.Value,
                    };

                    var result = await _orderdetailbusiness.Save(orderDetail);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var orderDetail = item.Data as OrderDetail;
                    orderDetail.OrderId = orderId;
                    orderDetail.WatchId = watchId;
                    orderDetail.Price = price;
                    orderDetail.Amount = amount;
                    orderDetail.Discount = discount;
                    orderDetail.Tax = tax;
                    orderDetail.ShippingCost = shippingCost;
                    orderDetail.ShipmentDate = shipmentDate.Value;
                    orderDetail.EstimatedDeliveryDate = estimatedDeliveryDate.Value;

                    var result = await _orderdetailbusiness.Update(orderDetail);
                    MessageBox.Show(result.Message, "Update");
                }

                // Clear textboxes and refresh grid
                txtOrderDetailId.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtWatchId.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtAmount.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                txtTax.Text = string.Empty;
                txtShipCost.Text = string.Empty;
                txtShipDate.SelectedDate = null;
                txtEstimatedDeliveryDate.SelectedDate = null;

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
            txtDiscount.Text = string.Empty;
            txtTax.Text = string.Empty;
            txtShipCost.Text = string.Empty;
            txtShipDate.SelectedDate = null;
            txtEstimatedDeliveryDate.SelectedDate = null;
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
                                txtDiscount.Text = orderDetail.Discount.ToString();
                                txtTax.Text = orderDetail.Tax.ToString();
                                txtShipCost.Text = orderDetail.ShippingCost.ToString();
                                txtShipDate.SelectedDate = orderDetail.ShipmentDate;
                                txtEstimatedDeliveryDate.SelectedDate = orderDetail.EstimatedDeliveryDate;
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
                                txtDiscount.Text = orderDetail.Discount.ToString();
                                txtTax.Text = orderDetail.Tax.ToString();
                                txtShipCost.Text = orderDetail.ShippingCost.ToString();
                                txtShipDate.SelectedDate = orderDetail.ShipmentDate;
                                txtEstimatedDeliveryDate.SelectedDate = orderDetail.EstimatedDeliveryDate;

                        }
                        }

                    }
                }
            }

   
    }
    }
