using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FuelCostCalculator
{
    public partial class HistoryPage : ContentPage
    {
        private readonly HistoryItemDb historyItemDb;

        public ObservableCollection<HistoryItemViewModel> HistoryItems { get; set; }
#pragma warning disable CA1416 // Validate platform compatibility
        public HistoryPage()
        {
            InitializeComponent();
            historyItemDb = new HistoryItemDb();
            HistoryItems = new ObservableCollection<HistoryItemViewModel>(); // Correctly initialize the collection
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadHistoryItems();
        }

        async Task LoadHistoryItems()
        {
            try
            {
                var items = await historyItemDb.GetHistoryItems();
                HistoryItems.Clear();
                foreach (var item in items)
                {
                    HistoryItems.Add(new HistoryItemViewModel(item));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load history items: {ex.Message}", "OK");
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

#pragma warning disable CA1416 // Validate platform compatibility
            await DisplayAlert("Details", message, "OK");

            ((ListView)sender).SelectedItem = null;

        }

        async void ClearButton_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Are you sure you want to clear the history?", "Yes", "No");
            if (confirm)
            {
                HistoryItems.Clear();
                await historyItemDb.ClearHistoryItems();
            }
        }
#pragma warning restore CA1416 // Validate platform compatibility
    }
}
