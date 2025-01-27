using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace map
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            var data = await FetchDataFromApiAsync();
            var sortedData = data.OrderBy(d => d.Datum).ThenBy(d => d.Tijd).ToList();
            listView.ItemsSource = sortedData;
        }

        private async Task<List<MyDataModel>> FetchDataFromApiAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://api.wiv.one/api/Waarnemingen"; // Replace with your Swagger UI API URL
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MyDataModel>>(responseBody);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Index Index = new Index();
            Index.Show();
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection change if needed
        }
    }

    public class MyDataModel
    {
        public int Wid { get; set; }
        public string Omschrijving { get; set; }
        public int? Sid { get; set; } = null;
        public DateTime Datum { get; set; }
        public TimeSpan Tijd { get; set; }
        public int? WNid { get; set; } = null;
        public int? Lid { get; set; } = null;
        public string Toelichting { get; set; }
        public int? Aantal { get; set; } = null;
        public string Geslacht { get; set; }
        public string Gebruiker { get; set; }
        public string Zekerheid { get; set; }
        public int? Webid { get; set; } = null;
        public string ManierDelen { get; set; }

        public string DatumTijd => $"{Datum:yyyy/MM/dd} {Tijd}";
    }

}
