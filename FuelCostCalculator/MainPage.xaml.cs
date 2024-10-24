using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace FuelCostCalculator
{
    public partial class MainPage : ContentPage
    {
        IConfiguration configuration;
        private double distance;
        private double fuelCostEuros;
        private double avgFuelConsumption;
        private double gasPrice;
        private int numberOfPeople;
        private double sharedCost;
        private readonly HistoryItemDb db;

        public MainPage(IConfiguration config)
        {
            InitializeComponent();
            configuration = config;
            db = new HistoryItemDb();
            GetFuelPrice();
        }

        private async Task<double> GetFuelPrice()
        {
            //string? url = configuration["FuelApiUrl"] ?? throw new InvalidOperationException("FuelApiUrl is not configured.");
            string url = "https://www.tankille.fi/suomi";
            try
            {
                if (button95.IsChecked)
                {
                    gasPrice = await FuelPriceScraper.GetFuel95Price(url);
                }
                else if (button98.IsChecked)
                {
                    gasPrice = await FuelPriceScraper.GetFuel98Price(url);
                }
                else if (buttonDiesel.IsChecked)
                {
                    gasPrice = await FuelPriceScraper.GetDieselPrice(url);
                }

                lblGasPrice.Text = $"Gas Price: {gasPrice:F3} €/l";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to fetch fuel prices: {ex.Message}", "OK");
            }

            return gasPrice;
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(TravelledDistance.Text, out distance) &&
                double.TryParse(AvgFuelConsumption.Text, out avgFuelConsumption))
            {
                CalculateFuelCost(distance, avgFuelConsumption, gasPrice);

                if (!string.IsNullOrEmpty(CostShareAmount.Text) && int.TryParse(CostShareAmount.Text, out numberOfPeople))
                {
                    CalculateSharedFuelCost(numberOfPeople);

                    await DisplayAlert("Result", $"Total fuel cost of travelled distance (€): {Math.Round(fuelCostEuros, 2)} €\n" +
                        $"Shared cost between {numberOfPeople} people (€): {Math.Round(sharedCost, 2)} €", "OK");

                    HistoryItem historyItem = new()
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
                    await DisplayAlert("Result", $"Total fuel cost of travelled distance (€): {Math.Round(fuelCostEuros, 2)} €", "OK");

                    HistoryItem historyItem = new()
                    {
                        Distance = distance,
                        AvgFuelConsumption = avgFuelConsumption,
                        GasPrice = gasPrice,
                        NumberOfPeople = 1,
                        SharedCost = fuelCostEuros
                    };
                    await db.AddHistoryItem(historyItem);
                }
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

        private async void OnFuelTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                await GetFuelPrice();
            }
        }

        private void HistoryBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }
    }
}