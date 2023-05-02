using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace APP.Android
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration(User user)
        {
            
            InitializeComponent();
            
            if (user.Name != null && user.LastName != null && user.Email != null && user.Phone != null)
            {
                txtId.Text = user.Id.ToString();
                txtName.Text = user.Name;
                txtLastname.Text = user.LastName;
                txtMobile.Text = user.Phone;
                txtEmail.Text = user.Email;

                txtId.IsVisible = false;
                BtnRegistration.IsVisible = false;
                BtnUpdate.IsVisible = true;
            } else {
                txtId.IsVisible = false;
                BtnRegistration.IsVisible = true;
                BtnUpdate.IsVisible = false;
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el nombre", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtLastname.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el apellido", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el email", "OK");
                return;

            }

            if (string.IsNullOrEmpty(txtMobile.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el teléfono", "OK");
                return;
            }

            try
            {
                BtnUpdate.IsEnabled = true;
                User userType = new User();
                userType.Id = int.Parse(txtId.Text);
                userType.Name = txtName.Text;
                userType.LastName = txtLastname.Text;
                userType.Email = txtEmail.Text;
                userType.Phone = txtMobile.Text;

                string url = "https://crudusersapi.azurewebsites.net/api/Users";

                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(userType);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);
                await DisplayAlert("Info", "Se ha editado el usuario", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
                BtnUpdate.IsEnabled = true;
                return;
            }

        }
        private async void BtnRegistration_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el nombre", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtLastname.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el apellido", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el email", "OK");
                return;

            }

            if (string.IsNullOrEmpty(txtMobile.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el teléfono", "OK");
                return;
            }

            try
            {
                BtnRegistration.IsEnabled = true;

                User userType = new User();
                userType.Name = txtName.Text;
                userType.LastName = txtLastname.Text;
                userType.Email = txtEmail.Text;
                userType.Phone = txtMobile.Text;

                string url = "https://crudusersapi.azurewebsites.net/api/Users";

                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(userType);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                if (result == "true")
                {
                    await DisplayAlert("Info", "Se ha registrado el usuario", "OK");

                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Message", "Error de registro", "ok");
                    return;
                }
            } catch (Exception ex) {
                await DisplayAlert("Message", ex.Message, "Ok");
                BtnRegistration.IsEnabled = true;
                return;
            }
        }
    }
}