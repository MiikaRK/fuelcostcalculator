using HtmlAgilityPack;
using System.Globalization;

namespace FuelCostCalculator
{
    internal static class FuelPriceScraper
    {
        public static async Task<double> GetFuel95Price(string url)
        {
            return await GetFuelPrice(url, "fuel-95");
        }

        public static async Task<double> GetFuel98Price(string url)
        {
            return await GetFuelPrice(url, "fuel-98");
        }

        public static async Task<double> GetDieselPrice(string url)
        {
            return await GetFuelPrice(url, "fuel-dsl");
        }

        private static async Task<double> GetFuelPrice(string url, string fuelId)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = await web.LoadFromWebAsync(url);

                var priceNode = doc.DocumentNode.SelectSingleNode($"//div[@id='{fuelId}']//h6[contains(text(), 'Keskiarvo')]");

                if (priceNode != null)
                {
                    string priceText = priceNode.InnerText.Replace("Keskiarvo", "").Trim();
                    priceText = priceText.Replace(",", ".").Trim();
                    priceText = new string(priceText.Where(c => char.IsDigit(c) || c == '.').ToArray());

                    if (double.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out double fuelPrice))
                    {
                        return Math.Round(fuelPrice, 3);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse price for {fuelId}: {priceText}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while fetching price for {fuelId}: {ex.Message}");
            }

            return 0;
        }
    }
}