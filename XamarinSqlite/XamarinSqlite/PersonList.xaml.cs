using XamarinSqlite.ViewModel;
using XamarinSqlite.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinSqlite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonList : ContentPage
	{
        PersonListViewModel personListViewModel;
		public PersonList()
		{
            personListViewModel = new PersonListViewModel(this.Navigation);
			InitializeComponent ();
            BindingContext = this.personListViewModel;
		}

        private void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Person person = (Person)e.SelectedItem;
            personListViewModel.SelectedPerson = person;
        }
    }
    
}