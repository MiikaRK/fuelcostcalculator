namespace FuelCostCalculator
{
    public partial class MainPage : ContentPage
    {
        private double distance;
        private double fuelCostEuros;
        private double avgFuelConsumption;
        private double gasPrice;
        private int numberOfPeople;
        private readonly double sharedCost;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(TravelledDistance.Text, out distance) && 
                double.TryParse(AvgFuelConsumption.Text, out avgFuelConsumption) && 
                double.TryParse(GasPrice.Text, out gasPrice))
            {
                distance = Convert.ToDouble(TravelledDistance.Text);
                avgFuelConsumption = Convert.ToDouble(AvgFuelConsumption.Text);
                gasPrice = Convert.ToDouble(GasPrice.Text);

                CalculateFuelCost(distance, avgFuelConsumption, gasPrice);

                if (CostShareAmount.Text != null && int.TryParse(CostShareAmount.Text, out numberOfPeople))
                {
                    CalculateSharedFuelCost(numberOfPeople, sharedCost, fuelCostEuros);
                }
            }
            else
            {
                await DisplayAlert("Incorrect input", "Please put valid input", "OK");
            }
        }
        private double CalculateFuelCost(double distance, double avgFuelConsumption, double gasPrice)
        {
            fuelCostEuros = (distance / 100) * avgFuelConsumption * gasPrice;
            FuelCostEur.Text = "Total fuel cost of travelled distance (€):\n" + Convert.ToString(Math.Round(fuelCostEuros, 2)) + "€";
            return fuelCostEuros;
        }

        private void CalculateSharedFuelCost(int numberOfPeople, double sharedCost, double fuelCostEuros)
        {
            numberOfPeople = Convert.ToInt32(CostShareAmount.Text);
            sharedCost = fuelCostEuros / numberOfPeople;
            SharedFuelCostEur.Text = $"Shared cost between {numberOfPeople} people (€):\n" + Convert.ToString(Math.Round(sharedCost, 2)) + "€";
        }
    }

}
