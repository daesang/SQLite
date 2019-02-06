using System;
using SQLite;

namespace Notes.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [Indexed]
        public string Text { get; set; }
        [Indexed]
        public DateTime Date { get; set; }
    }
}
