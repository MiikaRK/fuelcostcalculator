using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FuelCostCalculator
{
    public static class FuelPriceScraper
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<double> GetFuel95Price(string url)
        {
            var doc = await LoadFromUrl(url);
            var priceNode = doc.DocumentNode.SelectSingleNode("//h6[contains(text(), '95E10')]");
            return ExtractPrice(priceNode);
        }

        public static async Task<double> GetFuel98Price(string url)
        {
            var doc = await LoadFromUrl(url);
            var priceNode = doc.DocumentNode.SelectSingleNode("//h6[contains(text(), '98E5')]");
            return ExtractPrice(priceNode);
        }

        public static async Task<double> GetDieselPrice(string url)
        {
            var doc = await LoadFromUrl(url);
            var priceNode = doc.DocumentNode.SelectSingleNode("//h6[contains(text(), 'Diesel')]");
            return ExtractPrice(priceNode);
        }

        private static async Task<HtmlDocument> LoadFromUrl(string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throw if not a success code.

                var html = await response.Content.ReadAsStringAsync();
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw; // Optionally, rethrow the exception to be handled by the caller.
            }
        }

        private static double ExtractPrice(HtmlNode priceNode)
        {
            if (priceNode != null)
            {
                string priceText = priceNode.InnerText.Replace("Keskiarvo: ", "").Trim();
                if (double.TryParse(priceText, out double fuelPrice))
                {
                    return fuelPrice;
                }
            }
            throw new Exception("Failed to retrieve the fuel price");
        }
    }
}
