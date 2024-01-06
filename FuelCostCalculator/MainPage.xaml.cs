using System.Diagnostics;

namespace FuelCostCalculator
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        double fuelCostEuros;

        public MainPage()
        {
            InitializeComponent();
        }

        private void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            double distance = Convert.ToDouble(TravelledDistance.Text);
            double avgFuelConsumption = Convert.ToDouble(AvgFuelConsumption.Text);
            double gasPrice = Convert.ToDouble(GasPrice.Text);

            fuelCostEuros = (distance / 100) * avgFuelConsumption * gasPrice;
            FuelCostEur.Text = Convert.ToString(fuelCostEuros);
        }
    }

}
