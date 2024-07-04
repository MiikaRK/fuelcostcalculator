using SQLite;

namespace FuelCostCalculator
{
    public class HistoryItemDb
    {
        const string databaseFilename = "fuelCostDb.db3";
        static readonly string databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseFilename);
        const SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        SQLiteAsyncConnection database;

        public HistoryItemDb()
        {
            database = new SQLiteAsyncConnection(databasePath, flags);
        }

        async Task Init()
        {
            if (database == null)
            {
                database = new SQLiteAsyncConnection(databasePath, flags);
                await database.CreateTableAsync<HistoryItem>();
            }
        }

        public async Task<List<HistoryItem>> GetHistoryItems()
        {
            await Init();
            return await database.Table<HistoryItem>().ToListAsync();
        }

        public async Task AddHistoryItem(HistoryItem item)
        {
            await Init();
            await database.InsertAsync(item);
        }
        public async Task DeleteHistoryItem(HistoryItem item)
        {
            await Init();
            await database.DeleteAsync(item);
        }
        public async Task ClearHistoryItems()
        {
            await Init();
            await database.DropTableAsync<HistoryItem>();
            await database.CreateTableAsync<HistoryItem>();
        }
    }
}
