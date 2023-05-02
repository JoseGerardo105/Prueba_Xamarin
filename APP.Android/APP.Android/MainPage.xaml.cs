using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace APP.Android
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            GetUserInfo();
        }

        async void GetUserInfo()
        {
            try
            {
                string url = "https://crudusersapi.azurewebsites.net/api/Users";
                HttpClient client = new HttpClient();
                var result = await client.GetStringAsync(url);
                var EmpList = JsonConvert.DeserializeObject<List<User>>(result);
                UList.ItemsSource = null;
                UList.ItemsSource = new ObservableCollection<User> (EmpList);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Input Error", ex.Message, "OK");
                return;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            User user = new User();
            user.Name = null;
            user.LastName = null;
            user.Email = null;
            user.Phone = null;
            Navigation.PushAsync(new Registration(user));
        }

        private async void UList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                User user = e.Item as User;
                await Navigation.PushAsync(new Registration(user));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Input Error", ex.Message, "OK");
                return;
            }

        }

        private void UList_Refreshing(object sender, EventArgs e)
        {
            GetUserInfo();
            UList.IsRefreshing = false;
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var menu = sender as MenuItem;
                int userId = Convert.ToInt32(menu.CommandParameter.ToString());

                string url = $"https://crudusersapi.azurewebsites.net/api/Users?id={userId}";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(url);
                await DisplayAlert("Info", "Se ha eliminado el usuario", "OK");

                GetUserInfo();

            }
            catch(Exception ex) {
                await DisplayAlert("Error", ex.Message, "OK");
                return;
            }
        }

        
    }
}


