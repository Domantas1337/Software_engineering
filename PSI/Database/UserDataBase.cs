using PSI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI.Database
{
    public class UserDataBase
    {
        SQLiteAsyncConnection Database;

        public UserDataBase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<UserDataItem>();
        }


        public async Task<UserDataItem> GetItemAsync(string id)
        {
            await Init();
            return await Database.Table<UserDataItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        // public async Task<List<UserDataItem>> GetItemsNotDoneAsync()
        // {
        //     await Init();
        //    return await Database.Table<UserDataItem>().Where(t => t.Done).ToListAsync();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public async Task<int> SaveItemAsync(UserDataItem item)
        {
            await Init();
            if (item.Id != "")
            {
                Debug.Write(item.Id);
                return await Database.UpdateAsync(item);
            }
            else
            {
                Debug.Write(item.Id);
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(UserDataItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
