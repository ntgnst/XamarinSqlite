using Plugin.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinSqlite.Model;

namespace XamarinSqlite.ViewModel
{
    public class PersonListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Person> _personList;
        private INavigation _navigation;
        private Person _selectedPerson;
        public ObservableCollection<Person> PersonList
        {
            get { return _personList; }
            set
            {
                if (_personList != value)
                {
                    _personList = value;
                    OnPropertyChange(nameof(PersonList));
                }
            }
        }
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (_selectedPerson != null)
                {
                    _selectedPerson = value;
                    OnPropertyChange(nameof(SelectedPerson));
                }
            }
        }
        public Command Add { get; set; }
        public Command Delete { get; set; }
        public Command Edit { get; set; }
        public Command Confirm { get; set; }
        public Command Decline { get; set; }
        public Command Call { get; set; }
        public PersonListViewModel(INavigation navigation)
        {
            _selectedPerson = new Person();
            SelectedPerson = new Person();
            _navigation = navigation;
            PersonList = new ObservableCollection<Person>(App.DataAccess.GetAll().Result);
            _personList = new ObservableCollection<Person>(App.DataAccess.GetAll().Result);

            Add = new Command(async () =>
            {
                SelectedPerson = new Person();
                await _navigation.PushAsync(new PersonPage()
                {
                    Title = "Kişi Ekle",
                    BindingContext = this
                });
            });
            Delete = new Command(async () =>
            {
                if (SelectedPerson != null)
                {
                    if (SelectedPerson.ID == 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Uyarı", "Lütfen Bir Kayıt Seçiniz.", "Tamam");
                    }
                    else
                    {
                        bool result = await App.Current.MainPage.DisplayAlert("Sil", $"{SelectedPerson.Name}, İsimli Kayıt Silinecek, Emin misiniz ? ", "Evet", "Hayır");
                        if (result)
                        {
                            await App.DataAccess.Delete(_selectedPerson);
                            _personList.Remove(_selectedPerson);
                            _selectedPerson = new Person();

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert($"Hata !", $"{SelectedPerson.Name}, isimli kayıt silinemedi.", "Tamam");
                        }
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                }
            });
            Edit = new Command(async () =>
            {
                if (SelectedPerson != null)
                {
                    if (SelectedPerson.ID == 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Uyarı", "Lütfen Bir Kayıt Seçiniz.", "Tamam");
                    }
                    else
                    {
                        await _navigation.PushAsync(new PersonPage()
                        {
                            Title = "Kişi Düzenle",
                            BindingContext = this
                        });
                    }
                }
            });
            Confirm = new Command(async () =>
            {
                bool result = SelectedPerson != null ? true : false;
                if (result)
                {
                    if (_personList.Contains(SelectedPerson))
                    {
                        for (int i = 0; i < _personList.Count; i++)
                        {
                            if (_personList[i] == SelectedPerson)
                            {
                                PersonList[i] = SelectedPerson;
                                await App.DataAccess.Update(PersonList[i]);
                                SelectedPerson = new Person();
                            }
                        }
                    }
                    else
                    {
                        _selectedPerson.CreateTime = DateTime.Now;
                        _selectedPerson.LastUpdateTime = DateTime.Now;
                        _personList.Add(_selectedPerson);
                        await App.DataAccess.Insert(SelectedPerson);
                        SelectedPerson = new Person();
                    }
                }
                await App.Current.MainPage.Navigation.PopAsync();
            });
            Decline = new Command(async () =>
            {
                SelectedPerson = new Person();
                await App.Current.MainPage.Navigation.PopAsync();
            });
            Call = new Command(() =>
            {
                if (SelectedPerson != null && SelectedPerson.ID != 0)
                {
                    var dial = CrossMessaging.Current.PhoneDialer;
                    if (dial.CanMakePhoneCall)
                    {
                        dial.MakePhoneCall(SelectedPerson.GSM.ToString());
                    }
                }
                else
                    App.Current.MainPage.DisplayAlert("Hata", "Lütfen Bir Numara Seçiniz.", "Tamam");
            });
        }
        
        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
