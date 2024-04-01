using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelCostCalculator
{
    public class HistoryItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Distance { get; set; }
        public double AvgFuelConsumption { get; set; }
        public double GasPrice { get; set; }
        public int NumberOfPeople { get; set; }
        public double Cost { get; set; }

        public HistoryItemViewModel(HistoryItem item)
        {
            ID = item.ID;
            Date = item.Date;
            Distance = item.Distance;
            AvgFuelConsumption = item.AvgFuelConsumption;
            GasPrice = item.GasPrice;
            NumberOfPeople = item.NumberOfPeople;
            Cost = Math.Round(item.SharedCost, 2);
        }
    }
}
