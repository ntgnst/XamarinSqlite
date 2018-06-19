using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSqlite.Data;
using XamarinSqlite.Interface;
using XamarinSqlite.Model;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinSqlite
{
    public partial class App : Application
    {
        private static DataAccess _dataAccess;

        public static DataAccess DataAccess
        {
            get { return _dataAccess; }
        }
        
        public App()
        {
            _dataAccess = new DataAccess(DependencyService.Get<IFileHelper>().GetDBPath("test_db"));
            InitializeComponent();
            MainPage = new NavigationPage(new PersonList());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
