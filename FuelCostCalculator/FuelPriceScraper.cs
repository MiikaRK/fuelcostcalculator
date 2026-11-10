using System.Text.Json;

namespace FuelCostCalculator
{
    internal static class FuelPriceScraper
    {
        private static readonly HttpClient HttpClient = new();

        public static Task<double> GetFuel95Price(string url) => GetFuelPrice(url, 6, 10, "95 E10");

        public static Task<double> GetFuel98Price(string url) => GetFuelPrice(url, 7, 11, "98 E5");

        public static Task<double> GetDieselPrice(string url) => GetFuelPrice(url, 8, 12, "diesel");

        private static async Task<double> GetFuelPrice(string url, int priceIndex, int timestampIndex, string fuelName)
        {
            using var response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(stream);
            var root = document.RootElement;

            if (!root.TryGetProperty("fresh_since", out var freshSinceElement) ||
                !freshSinceElement.TryGetInt64(out long freshSince) ||
                !root.TryGetProperty("stations", out var stations) ||
                stations.ValueKind != JsonValueKind.Array)
            {
                throw new InvalidDataException("Litrahinta returned an unexpected station-data format.");
            }

            long totalPriceMilliEuros = 0;
            int stationCount = 0;

            foreach (var station in stations.EnumerateArray())
            {
                if (station.ValueKind != JsonValueKind.Array || station.GetArrayLength() <= timestampIndex)
                {
                    continue;
                }

                var country = station[9];
                var price = station[priceIndex];
                var timestamp = station[timestampIndex];

                if (country.ValueKind != JsonValueKind.String || country.GetString() != "FI" ||
                    price.ValueKind != JsonValueKind.Number || !price.TryGetInt32(out int priceMilliEuros) || priceMilliEuros <= 0 ||
                    timestamp.ValueKind != JsonValueKind.Number || !timestamp.TryGetInt64(out long reportedAt) || reportedAt < freshSince)
                {
                    continue;
                }

                totalPriceMilliEuros += priceMilliEuros;
                stationCount++;
            }

            if (stationCount == 0)
            {
                throw new InvalidDataException($"Litrahinta returned no recent prices for {fuelName}.");
            }

            double averagePrice = totalPriceMilliEuros / (double)stationCount / 1000;
            return Math.Round(averagePrice, 3, MidpointRounding.AwayFromZero);
        }
    }
}