using System.ComponentModel;

namespace FuelCostCalculator
{
    public class HistoryItemViewModel(HistoryItem item) : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int ID { get; set; } = item.ID;
        public DateTime Date { get; set; } = item.Date;
        public double Distance { get; set; } = item.Distance;
        public double AvgFuelConsumption { get; set; } = item.AvgFuelConsumption;
        public double GasPrice { get; set; } = item.GasPrice;
        public int NumberOfPeople { get; set; } = item.NumberOfPeople;
        public double Cost { get; set; } = Math.Round(item.SharedCost, 2);

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}