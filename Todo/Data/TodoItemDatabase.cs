using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Todo.Models;

namespace Todo.Data
{
    public class TodoItemDatabase
    {
        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer =
            new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags));

        private static SQLiteAsyncConnection Database => lazyInitializer.Value;
        private static bool initialized;

        public TodoItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        private static async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (Database.TableMappings.All(m => m.MappedType.Name != nameof(TodoItem)))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TodoItem)).ConfigureAwait(true);
                    initialized = true;
                }
            }
        }

        public static Task<List<TodoItem>> GetItemsAsync()
        {
            return Database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public static Task<int> SaveItemAsync(TodoItem item)
        {
            return item.ID != 0 ? Database.UpdateAsync(item) : Database.InsertAsync(item);
        }

        public static Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}

