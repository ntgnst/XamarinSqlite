
using SQLite;
using System;

namespace XamarinSqlite.Model
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string  Surname { get; set; }
        [NotNull]
        public Int64 GSM { get; set; }
        [NotNull]
        public DateTime CreateTime { get; set; }
        [NotNull]
        public DateTime LastUpdateTime { get; set; }

    }
}
