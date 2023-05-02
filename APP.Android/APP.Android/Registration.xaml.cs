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
                txtName.Text = user.Name;
                txtLastname.Text = user.LastName;
                txtEmail.Text = user.Email;
                txtMobile.Text = user.Phone;

                txtMobile.IsEnabled = false;
                BtnRegistration.IsVisible = false;
                BtnUpdate.IsVisible = true;
            } else {
                txtMobile.IsEnabled = true;
                BtnRegistration.IsVisible = true;
                BtnUpdate.IsVisible = false;
                
            }


        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            /*
            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Input Error", "Se requiere el id", "OK");
                return;
            }*/
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
                BtnUpdate.IsEnabled = false;
                User userType = new User();
                userType.Name = txtName.Text;
                userType.LastName = txtLastname.Text;
                userType.Email = txtEmail.Text;
                userType.Phone = txtMobile.Text;

                string url = "https://crudusersapi.azurewebsites.net/api/Users";

                HttpClient client = new HttpClient();
                string jsonData = JsonConvert.SerializeObject(userType);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
                if (responseData.Status == 1)
                {
                    await DisplayAlert("Message", responseData.Message, "ok");
                    BtnUpdate.IsEnabled = true;
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Message", responseData.Message, "ok");
                    BtnRegistration.IsEnabled = true;
                    return;

                }
                BtnRegistration.IsEnabled = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "Ok");
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
                BtnRegistration.IsEnabled = false;
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
                Response responseData = JsonConvert.DeserializeObject<Response>(result);
                if (responseData.Status == 1)
                {
                    await DisplayAlert("Message", "Hola", "ok");

                    await Navigation.PopAsync();
                    BtnRegistration.IsEnabled = true;
                }
                else
                {
                    await DisplayAlert("Message", responseData.Message, "ok");
                    BtnRegistration.IsEnabled = true;
                    return;

                }
                BtnRegistration.IsEnabled = true;
            } catch (Exception ex) {
                await DisplayAlert("Message", ex.Message, "Ok");
                BtnRegistration.IsEnabled = true;
                return;
            }

        }

    }
}