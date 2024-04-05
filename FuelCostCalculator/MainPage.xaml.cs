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
        HistoryItemDb db;

        public MainPage()
        {
            InitializeComponent();
            db = new HistoryItemDb();
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

                string resultMessage = "Total fuel cost of travelled distance (€):\n"
                    + Convert.ToString(Math.Round(fuelCostEuros, 2)) + "€";

                if (CostShareAmount.Text != null && int.TryParse(CostShareAmount.Text, out numberOfPeople))
                {
                    CalculateSharedFuelCost(numberOfPeople);

                    resultMessage += $"\nShared cost between {numberOfPeople} people (€):\n"
                        + Convert.ToString(Math.Round(sharedCost, 2)) + "€";

                    HistoryItem historyItem = new HistoryItem
                    {
                        Distance = distance,
                        AvgFuelConsumption = avgFuelConsumption,
                        GasPrice = gasPrice,
                        NumberOfPeople = numberOfPeople,
                        SharedCost = sharedCost
                    };
                    await db.AddHistoryItem(historyItem);
                }
                else
                {
                    numberOfPeople = 1;
                    sharedCost = fuelCostEuros;

                    HistoryItem historyItem = new HistoryItem
                    {
                        Distance = distance,
                        AvgFuelConsumption = avgFuelConsumption,
                        GasPrice = gasPrice,
                        NumberOfPeople = numberOfPeople,
                        SharedCost = sharedCost
                    };
                    await db.AddHistoryItem(historyItem);
                }

                await DisplayAlert("Result", resultMessage, "OK");
            }
            else
            {
                await DisplayAlert("Error", "Incorrect input, try again", "OK");
            }
        }

        private void CalculateFuelCost(double distance, double avgFuelConsumption, double gasPrice)
        {
            fuelCostEuros = (distance / 100) * avgFuelConsumption * gasPrice;
        }

        private void CalculateSharedFuelCost(int numberOfPeople)
        {
            sharedCost = fuelCostEuros / numberOfPeople;
        }

        private void HistoryBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }
    }
}