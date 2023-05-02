using App.Android.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.Android
{
    public partial class MainPage : ContentPage
    {
        private string url = "https://localhost:7072/api/Users";
        HttpClient client = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            string contenido = await client.GetStringAsync(url);
            IEnumerable<UsuarioModel> lista = JsonConvert.DeserializeObject<IEnumerable<UsuarioModel>>(contenido);
            ltusuario.ItemsSource = new ObservableCollection<UsuarioModel>(lista);
            base.OnAppearing();
        }
    }
}
