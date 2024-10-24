using SQLite;

namespace FuelCostCalculator
{
    public class HistoryItemDb
    {
        const string databaseFilename = "fuelCostDb.db3";
        static readonly string databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseFilename);
        const SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        private readonly SQLiteAsyncConnection database;

        public HistoryItemDb()
        {
            database = new SQLiteAsyncConnection(databasePath, flags);
            _ = InitDatabase();
        }

        private async Task InitDatabase()
        {
            await database.CreateTableAsync<HistoryItem>();
        }

        public async Task<List<HistoryItem>> GetHistoryItems()
        {
            return await database.Table<HistoryItem>().ToListAsync();
        }

        public async Task AddHistoryItem(HistoryItem item)
        {
            await database.InsertAsync(item);
        }

        public async Task DeleteHistoryItem(HistoryItem item)
        {
            await database.DeleteAsync(item);
        }

        public async Task ClearHistoryItems()
        {
            await database.DeleteAllAsync<HistoryItem>();
        }
    }
}