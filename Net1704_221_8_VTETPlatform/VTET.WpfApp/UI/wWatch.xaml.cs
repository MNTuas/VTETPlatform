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
    /// Interaction logic for wWatch.xaml
    /// </summary>
    public partial class wWatch : Window
    {
        public readonly IWatchBusiness _watchBusiness;

        public wWatch()
        {
            InitializeComponent();
            this._watchBusiness ??= new watchBusiness();
            this.LoadGrdWatch();
        }

        private async void LoadGrdWatch()
        {
            var result = await _watchBusiness.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdWatch.ItemsSource = result.Data as List<Watch>;
            }
            else
            {
                grdWatch.ItemsSource = new List<Watch>();
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int watchIdTmp = -1;
                decimal price, estimatePrice;
                string condition, location, brand, status, fullName, type;
                DateTime createDate;
                int customerId;

                // Validate and parse inputs
                

                fullName = txtWatchFullName.Text;
                type = txtWatchType.Text;

                if (!decimal.TryParse(txtPrice.Text, out price))
                {
                    MessageBox.Show("Invalid Price", "Error");
                    return;
                }

                if (!decimal.TryParse(txtEstimatePrice.Text, out estimatePrice))
                {
                    MessageBox.Show("Invalid EstimatePrice", "Error");
                    return;
                }

                condition = txtCondition.Text;
                location = txtLocation.Text;
                brand = txtBrand.Text;
                status = txtStatus.Text;

                if (!DateTime.TryParse(txtCreateDate.Text, out createDate))
                {
                    MessageBox.Show("Invalid CreateDate", "Error");
                    return;
                }

                if (!int.TryParse(txtCustomer_ID.Text, out customerId))
                {
                    MessageBox.Show("Invalid CustomerId", "Error");
                    return;
                }
                int.TryParse(txtId.Text, out watchIdTmp);
                var item = await _watchBusiness.GetById(watchIdTmp);

                if (item.Data == null)
                {
                    var watch = new Watch
                    {
                        Id = watchIdTmp,
                        FullName = fullName,
                        Type = type,
                        Price = price,
                        EstimatePrice = estimatePrice,
                        Condition = condition,
                        Location = location,
                        Brand = brand,
                        Status = status,
                        CreateDate = createDate,
                        CustomerId = customerId
                    };

                    var result = await _watchBusiness.Save(watch);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var watch = item.Data as Watch;
                    watch.FullName = fullName;
                    watch.Type = type;
                    watch.Price = price;
                    watch.EstimatePrice = estimatePrice;
                    watch.Condition = condition;
                    watch.Location = location;
                    watch.Brand = brand;
                    watch.Status = status;
                    watch.CreateDate = createDate;
                    watch.CustomerId = customerId;

                    var result = await _watchBusiness.Update(watch);
                    MessageBox.Show(result.Message, "Update");
                }

                // Clear textboxes and refresh grid
                ClearWatchForm();
                this.LoadGrdWatch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ClearWatchForm()
        {
            txtId.Text = string.Empty;
            txtWatchFullName.Text = string.Empty;
            txtWatchType.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtEstimatePrice.Text = string.Empty;
            txtCondition.Text = string.Empty;
            txtLocation.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtCreateDate.SelectedDate = null;
            txtCustomer_ID.Text = string.Empty;
        }

        private async void grdWatch_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int id)
            {
                var result = await _watchBusiness.Delete(id);
                if (result.Status > 0)
                {
                    MessageBox.Show(result.Message, "Delete");
                    LoadGrdWatch();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error");
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearWatchForm();
        }

        private async void grdWatch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Watch;
                    var watchResult = await _watchBusiness.GetById(item.Id);

                    if (watchResult.Status > 0 && watchResult.Data != null)
                    {
                        var watch = watchResult.Data as Watch;
                        if (watch != null)
                        {
                            txtId.Text = watch.Id.ToString();
                            txtWatchFullName.Text = watch.FullName;
                            txtWatchType.Text = watch.Type;
                            txtPrice.Text = watch.Price.ToString();
                            txtEstimatePrice.Text = watch.EstimatePrice.ToString();
                            txtCondition.Text = watch.Condition;
                            txtLocation.Text = watch.Location;
                            txtBrand.Text = watch.Brand;
                            txtStatus.Text = watch.Status;
                            txtCreateDate.SelectedDate = watch.CreateDate;
                            txtCustomer_ID.Text = watch.CustomerId.ToString();
                        }
                    }
                }
            }
        }
    }
}
