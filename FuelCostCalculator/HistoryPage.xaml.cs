using System.Collections.ObjectModel;

namespace FuelCostCalculator
{
    public partial class HistoryPage : ContentPage
    {
        private readonly HistoryItemDb historyItemDb;

        public ObservableCollection<HistoryItemViewModel> HistoryItems { get; set; }

        public HistoryPage()
        {
            InitializeComponent();
            historyItemDb = new HistoryItemDb();
            HistoryItems = [];
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadHistoryItems();
        }

        async Task LoadHistoryItems()
        {
            var items = await historyItemDb.GetHistoryItems();
            HistoryItems.Clear();
            foreach (var item in items)
            {
                HistoryItems.Add(new HistoryItemViewModel(item));
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is not HistoryItemViewModel selectedItem)
                return;

            string message = $"Date: {selectedItem.Date}\n\n" +
                             $"Distance (km): {selectedItem.Distance}\n" +
                             $"Average fuel consumption (l/100km): {selectedItem.AvgFuelConsumption}\n" +
                             $"Price of the gas (€): {selectedItem.GasPrice}\n" +
                             $"Number of people sharing the fuel cost: {selectedItem.NumberOfPeople}\n" +
                             $"Cost (€): {Math.Round(selectedItem.Cost, 2)}";

            await DisplayAlert("Details", message, "OK");

            ((ListView)sender).SelectedItem = null;
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            HistoryItems.Clear();
            _ = historyItemDb.ClearHistoryItems();
        }
    }
}
