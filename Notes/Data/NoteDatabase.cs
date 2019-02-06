using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Notes.Models;
using System;
using System.Diagnostics;

namespace Notes.Data
{
    public class NoteDatabase
    {
        object locker = new object(); // class level private field
        readonly SQLiteAsyncConnection _database;

        public NoteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTablesAsync<Note, Note2>().Wait();
        }

        public async Task<IList<T>> GetNotesAsync<T>() where T : class, new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<T> GetNoteAsync<T>(int id) where T : class, new()
        {
            return await _database.GetAsync<T>(id);

            //return _database.Table<Note>()
            //                .Where(i => i.ID == id)
            //                .FirstOrDefaultAsync();
        }

        public async Task<IList<T>> GetObject<T>(string key) where T : class, new()
        {
            try
            {
                var items = await _database.QueryAsync<T>(string.Format("SELECT * FROM {0} WHERE Text like '{1}%';", typeof(T).Name, key));
                return items;
            }
            catch (Exception e)
            {
            }
            return default(IList<T>);
        }


        public async Task<string> InsertOrReplaceAsync<T>(T item)
        {
            await _database.InsertOrReplaceAsync(item, typeof(T));

            return string.Empty;
        }

        public async Task<string> SaveNoteAsync<T>(T item, int id)
        {

            if (await _database.UpdateAsync(item, typeof(T)) == 0)
            {
                await _database.InsertAsync(item, typeof(T));
            }

            //if (await _database.InsertAsync(item, typeof(T)) != 0)
            //{
            //    await _database.UpdateAsync(item, typeof(T));
            //}

            //if(id != 0)
            //{
            //    await _database.InsertAsync(item, typeof(T));
            //}
            //else
            //{
            //    await _database.UpdateAsync(item, typeof(T));
            //}

            return string.Empty;
        }


        public async Task<string> UpdateNoteAsync<T>(T item)
        {
            await _database.UpdateAsync(item, typeof(T));

            return string.Empty;
        }

        public async Task<string> InsertNoteAsync<T>(T item)
        {
            try
            {
                await _database.InsertAsync(item, typeof(T));
            }
            catch(Exception e)
            {
                return e.Message;
            }
            
            return string.Empty;
        }


        public async Task<int> DeleteNoteAsync<T>(int id)
        {
            return await _database.DeleteAsync<T>(id);
        }
    }
}
