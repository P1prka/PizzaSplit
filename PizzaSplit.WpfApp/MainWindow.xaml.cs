using System.Globalization;
using System.Windows;
using PizzaSplit.Core;

namespace PizzaSplit.WpfApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Calculate_Click(object sender, RoutedEventArgs e)
    {
        ResultTextBlock.Text = "";
        ErrorTextBlock.Text = "";

        CultureInfo culture = CultureInfo.GetCultureInfo("et-EE");

        if (!decimal.TryParse(
                TotalTextBox.Text,
                NumberStyles.Number,
                culture,
                out decimal total))
        {
            ErrorTextBlock.Text = "Sisesta õige summa.";
            return;
        }

        if (!int.TryParse(PeopleTextBox.Text, out int people))
        {
            ErrorTextBlock.Text = "Sisesta õige sööjate arv.";
            return;
        }

        bool addTip = TipCheckBox.IsChecked == true;

        if (BillCalculator.TryCalculate(
                total,
                people,
                addTip,
                out decimal share,
                out string error))
        {
            ResultTextBlock.Text =
                $"Mina maksan {share.ToString("F2", culture)} €";
        }
        else
        {
            ErrorTextBlock.Text = error;
        }
    }
    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        TotalTextBox.Clear();
        PeopleTextBox.Clear();
        TipCheckBox.IsChecked = false;
        ResultTextBlock.Text = "";
        ErrorTextBlock.Text = "";
        TotalTextBox.Focus();
    }
}