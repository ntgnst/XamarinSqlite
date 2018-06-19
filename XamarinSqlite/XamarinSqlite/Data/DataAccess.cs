using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinSqlite.Model;

namespace XamarinSqlite.Data
{
    public class DataAccess
    {
        SQLiteAsyncConnection dbConn;
        public DataAccess(string path)
        {
            dbConn = new SQLiteAsyncConnection(path);
            dbConn.CreateTableAsync<Person>();
        }
        public Task<List<Person>> GetAll()
        {
            return dbConn.Table<Person>().ToListAsync();
        }
        public Task<int> Insert(Person model)
        {
            return dbConn.InsertAsync(model);
        }
        public Task<int> Delete(Person model)
        {
            return dbConn.DeleteAsync(model);
        }
        public Task<int> Update(Person model)
        {
            return dbConn.UpdateAsync(model);
        }
    }
}
