using SQLite;

namespace FuelCostCalculator
{
    public class HistoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Distance { get; set; }
        public double AvgFuelConsumption { get; set; }
        public double GasPrice { get; set; }
        public int NumberOfPeople { get; set; }
        public double SharedCost { get; set; }

        public HistoryItem()
        {
            Date = DateTime.Now;
        }
    }
}