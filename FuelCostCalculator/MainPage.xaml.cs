namespace FuelCostCalculator
{
    public partial class MainPage : ContentPage
    {
        private double _distance;
        private double _fuelCostEuros;
        private double _avgFuelConsumption;
        private double _gasPrice;
        private int _numberOfPeople;
        private double _sharedCost;
        private readonly HistoryItemDb _db;

        public MainPage()
        {
            InitializeComponent();
            _db = new HistoryItemDb();
            _ = GetFuelPriceAsync();
        }

        private async Task<double> GetFuelPriceAsync()
        {
            string url = Properties.Resources.FuelApiUrl;
            if (string.IsNullOrWhiteSpace(url))
            {
                await DisplayAlert("Error", "FuelApiUrl is not configured or is empty.", "OK");
                throw new InvalidOperationException("FuelApiUrl is not configured or is empty.");
            }

            try
            {
                if (button95.IsChecked)
                {
                    _gasPrice = await FuelPriceScraper.GetFuel95Price(url);
                }
                else if (button98.IsChecked)
                {
                    _gasPrice = await FuelPriceScraper.GetFuel98Price(url);
                }
                else if (buttonDiesel.IsChecked)
                {
                    _gasPrice = await FuelPriceScraper.GetDieselPrice(url);
                }

                lblGasPrice.Text = $"Gas Price: {_gasPrice:F3} €/l";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch fuel prices: {ex.Message}", "OK");
            }

            return _gasPrice;
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(TravelledDistance.Text, out _distance) &&
                double.TryParse(AvgFuelConsumption.Text, out _avgFuelConsumption))
            {
                CalculateFuelCost(_distance, _avgFuelConsumption, _gasPrice);

                if (!string.IsNullOrEmpty(CostShareAmount.Text) && int.TryParse(CostShareAmount.Text, out _numberOfPeople))
                {
                    CalculateSharedFuelCost(_numberOfPeople);

                    await DisplayAlert("Result", $"Total fuel cost of travelled distance (€): {Math.Round(_fuelCostEuros, 2)} €\n" +
                        $"Shared cost between {_numberOfPeople} people (€): {Math.Round(_sharedCost, 2)} €", "OK");

                    HistoryItem historyItem = new()
                    {
                        Distance = _distance,
                        AvgFuelConsumption = _avgFuelConsumption,
                        GasPrice = _gasPrice,
                        NumberOfPeople = _numberOfPeople,
                        SharedCost = _sharedCost
                    };
                    await _db.AddHistoryItem(historyItem);
                }
                else
                {
                    await DisplayAlert("Result", $"Total fuel cost of travelled distance (€): {Math.Round(_fuelCostEuros, 2)} €", "OK");

                    HistoryItem historyItem = new()
                    {
                        Distance = _distance,
                        AvgFuelConsumption = _avgFuelConsumption,
                        GasPrice = _gasPrice,
                        NumberOfPeople = 1,
                        SharedCost = _fuelCostEuros
                    };
                    await _db.AddHistoryItem(historyItem);
                }
            }
            else
            {
                await DisplayAlert("Error", "Incorrect input, try again", "OK");
            }
        }

        private void CalculateFuelCost(double distance, double avgFuelConsumption, double gasPrice)
        {
            _fuelCostEuros = (distance / 100) * avgFuelConsumption * gasPrice;
        }

        private void CalculateSharedFuelCost(int numberOfPeople)
        {
            _sharedCost = _fuelCostEuros / numberOfPeople;
        }

        private async void OnFuelTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                await GetFuelPriceAsync();
            }
        }

        private void HistoryBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }
    }
}