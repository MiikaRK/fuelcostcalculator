using Microsoft.Extensions.Configuration;
using System;

namespace FuelCostCalculator
{
    public partial class MainPage : ContentPage
    {
        private readonly IConfiguration _configuration;
        private double distance;
        private double fuelCostEuros;
        private double avgFuelConsumption;
        private double gasPrice;
        private int numberOfPeople;
        private double sharedCost;
        readonly HistoryItemDb db;

        public MainPage(IConfiguration configuration)
        {
            InitializeComponent();
            _configuration = configuration;
            db = new HistoryItemDb();
        }

        private async void OnFuelTypeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                try
                {
                    //string? url = _configuration["FuelApiUrl"] ?? throw new InvalidOperationException("FuelApiUrl is not configured.");
                    string url = "https://www.tankille.fi/suomi/";
                    var selectedRadioButton = (RadioButton)sender;
                    string selectedFuelType = selectedRadioButton.Content?.ToString() ?? throw new InvalidOperationException("Selected fuel type is null.");

                    switch (selectedFuelType)
                    {
                        case "95E10":
                            gasPrice = await FuelPriceScraper.GetFuel95Price(url);
                            break;
                        case "98E5":
                            gasPrice = await FuelPriceScraper.GetFuel98Price(url);
                            break;
                        case "Diesel":
                            gasPrice = await FuelPriceScraper.GetDieselPrice(url);
                            break;
                    }

                    lblGasPrice.Text = $"Gas Price: {gasPrice} €/l";
                }
                catch (Exception ex)
                {
                    lblGasPrice.Text = $"Error fetching gas price: {ex.Message}";
                }
            }
        }

        private async void SubmitBtn_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(TravelledDistance.Text, out distance) &&
                double.TryParse(AvgFuelConsumption.Text, out avgFuelConsumption))
            {
                distance = Convert.ToDouble(TravelledDistance.Text);
                avgFuelConsumption = Convert.ToDouble(AvgFuelConsumption.Text);

                CalculateFuelCost(distance, avgFuelConsumption, gasPrice);

                if (CostShareAmount.Text != null && int.TryParse(CostShareAmount.Text, out numberOfPeople))
                {
                    CalculateSharedFuelCost(numberOfPeople);

                    await DisplayAlert("Result", "Total fuel cost of travelled distance (€):\n"
                        + Convert.ToString(Math.Round(fuelCostEuros, 2)) + "€\n"
                        + $"Shared cost between {numberOfPeople} people (€):\n"
                        + Convert.ToString(Math.Round(sharedCost, 2)) + "€", "OK");

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
                    await DisplayAlert("Result", "Total fuel cost of travelled distance (€):\n"
                        + Convert.ToString(Math.Round(fuelCostEuros, 2)) + "€", "OK");

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

        private void HistoryBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }
    }
}