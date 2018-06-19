using System;
using System.IO;
using Xamarin.Forms;
using XamarinSqlite.Droid;
using XamarinSqlite.Interface;

[assembly: Dependency(typeof(FileHelper))]
namespace XamarinSqlite.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetDBPath(string dbName)
        {
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(dbPath, dbName);
        }
    }
}