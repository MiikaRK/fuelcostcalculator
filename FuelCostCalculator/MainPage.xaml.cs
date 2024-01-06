using System.Diagnostics;

namespace FuelCostCalculator
{
    public partial class MainPage : ContentPage
    {
        private double distance;
        private double fuelCostEuros;
        private double avgFuelConsumption;
        private double gasPrice;
        private int numberOfPeople;
        private double sharedCost;

        public MainPage()
        {
            InitializeComponent();
        }

        private void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            distance = Convert.ToDouble(TravelledDistance.Text);
            avgFuelConsumption = Convert.ToDouble(AvgFuelConsumption.Text);
            gasPrice = Convert.ToDouble(GasPrice.Text);

            fuelCostEuros = (distance / 100) * avgFuelConsumption * gasPrice;
            FuelCostEur.Text = "Total fuel cost of travelled distance (€):\n" + Convert.ToString(Math.Round(fuelCostEuros, 2)) + "€";

            if (CostShareAmount.Text != null)
            {
                numberOfPeople = Convert.ToInt32(CostShareAmount.Text);
                sharedCost = fuelCostEuros / numberOfPeople;
                SharedFuelCostEur.Text = $"Shared cost between {numberOfPeople} people (€):\n" + Convert.ToString(Math.Round(sharedCost, 2)) + "€";
            }
        }
    }

}
