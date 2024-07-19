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
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using VTET.Data.Models;
using VTET.Business;
using System.ComponentModel.DataAnnotations;

namespace VTET.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wEvaluation.xaml
    /// </summary>
    public partial class wEvaluation : Window
    {
        public readonly IEvaluationBusiness _evaluationbusiness;
        public wEvaluation()
        {
            InitializeComponent();
            this._evaluationbusiness ??= new evaluationBusiness();
            this.LoadGrdEvaluation();
        }
        private async void LoadGrdEvaluation()
        {
            var result = await _evaluationbusiness.GetAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdEvaluation.ItemsSource = result.Data as List<Evaluation>;
            }
            else
            {
                grdEvaluation.ItemsSource = new List<Evaluation>();
            }
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int evaluationIdTmp = -1;
                int.TryParse(txtEvaluationId.Text, out evaluationIdTmp);

                var item = await _evaluationbusiness.GetById(evaluationIdTmp);
                Evaluation evaluation;

                if (item.Data == null)
                {
                    evaluation = new Evaluation
                    {
                        Id = evaluationIdTmp,
                        Comment = txtComment.Text,
                        Rate = int.Parse(txtRate.Text),
                        EstimatePrice = decimal.Parse(txtEstimatePrice.Text),
                        CreateDate = DateTime.Now,
                        WatchId = int.Parse(txtWatchId.Text),
                    };
                }
                else
                {
                    evaluation = item.Data as Evaluation;
                    evaluation.Comment = txtComment.Text;
                    evaluation.Rate = int.Parse(txtRate.Text);
                    evaluation.EstimatePrice = decimal.Parse(txtEstimatePrice.Text);
                    evaluation.WatchId = int.Parse(txtWatchId.Text);
                }

                // Validate the evaluation object
                var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(evaluation);
                if (!System.ComponentModel.DataAnnotations.Validator.TryValidateObject(evaluation, context, validationResults, true))
                {
                    string errorMessages = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                    MessageBox.Show(errorMessages, "Validation Error");
                    return;
                }

                if (item.Data == null)
                {
                    var result = await _evaluationbusiness.Save(evaluation);
                    MessageBox.Show(result.Message, "Save");
                }
                else
                {
                    var result = await _evaluationbusiness.Update(evaluation);
                    MessageBox.Show(result.Message, "Update");
                }

                txtEvaluationId.Text = string.Empty;
                txtComment.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtEstimatePrice.Text = string.Empty;
                txtCreateDate.Text = string.Empty;
                txtWatchId.Text = string.Empty;
                LoadGrdEvaluation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private async void grdEvaluation_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int id)
            {
                var result = await _evaluationbusiness.Delete(id);
                if (result.Status > 0)
                {
                    MessageBox.Show(result.Message, "Delete");
                    LoadGrdEvaluation();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error");
                }
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            txtEvaluationId.Text = string.Empty;
            txtComment.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtEstimatePrice.Text = string.Empty;
            txtCreateDate.Text = string.Empty;
            txtWatchId.Text = string.Empty;
            this.LoadGrdEvaluation();
        }
        private async Task grdEvaluation_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Evaluation;
                    var evaluationResult = await _evaluationbusiness.GetById(item.Id);

                    if (evaluationResult.Status > 0 && evaluationResult.Data != null)
                    {
                        var evaluation = evaluationResult.Data as Evaluation;
                        if (evaluation != null)
                        {
                            txtEvaluationId.Text = evaluation.Id.ToString();
                            txtComment.Text = evaluation.Comment.ToString();
                            txtRate.Text = evaluation.Rate.ToString();
                            txtEstimatePrice.Text = evaluation.EstimatePrice.ToString();
                            txtCreateDate.Text = evaluation.CreateDate.ToString();
                            txtWatchId.Text = evaluation.WatchId.ToString();
                        }
                    }

                }
            }
        }
        private async void grdevaluation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Double Click on Grid");
            DataGrid grd = sender as DataGrid;
            if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
            {
                var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
                if (row != null)
                {
                    var item = row.Item as Evaluation;
                    var evaluationResult = await _evaluationbusiness.GetById(item.Id);

                    if (evaluationResult.Status > 0 && evaluationResult.Data != null)
                    {
                        var evaluation = evaluationResult.Data as Evaluation;
                        if (evaluation != null)
                        {
                            txtEvaluationId.Text = evaluation.Id.ToString();
                            txtComment.Text = evaluation.Comment.ToString();
                            txtRate.Text = evaluation.Rate.ToString();
                            txtEstimatePrice.Text = evaluation.EstimatePrice.ToString();
                            txtCreateDate.Text = evaluation.CreateDate.ToString();
                            txtWatchId.Text = evaluation.WatchId.ToString();
                        }
                    }

                }
            }
        }
    }
}
