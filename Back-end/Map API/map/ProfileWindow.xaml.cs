using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            var waarnemingen = await FetchWaarnemingenFromApiAsync("https://api.wiv.one/api/Waarnemingen");
            var soorten = await FetchSoortenFromApiAsync("https://api.wiv.one/api/Soorten");

            var sortedData = waarnemingen.OrderBy(d => d.Datum).ThenBy(d => d.Tijd).ToList();

            // Combineer de gegevens uit de waarnemingen en soorten API's
            foreach (var waarneming in sortedData)
            {
                var soort = soorten.FirstOrDefault(s => s.Sid == waarneming.Sid);
                if (soort != null)
                {
                    waarneming.Voorkomen = soort.Voorkomen;
                }
            }

            listView.ItemsSource = sortedData;
        }

        private async Task<List<MyDataModel>> FetchWaarnemingenFromApiAsync(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MyDataModel>>(responseBody);
            }
        }

        private async Task<List<SoortDataModel>> FetchSoortenFromApiAsync(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<SoortDataModel>>(responseBody);
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
            if (listView.SelectedItem != null)
            {
                deleteButton.Background = new SolidColorBrush(Colors.Red);
                deleteButton.IsEnabled = true;
            }
            else
            {
                deleteButton.Background = new SolidColorBrush(Colors.Gray);
                deleteButton.IsEnabled = false;
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = listView.SelectedItem as MyDataModel;
            if (selectedItem != null)
            {
                var result = MessageBox.Show("Weet je het zeker?", "Bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    bool isDeleted = await DeleteWaarnemingAsync(selectedItem.Wid);
                    if (isDeleted)
                    {
                        Console.WriteLine("Waarneming succesvol verwijderd.");
                        // Verwijder het item uit de ListView zonder de hele pagina te verversen
                        var items = listView.ItemsSource as List<MyDataModel>;
                        items.Remove(selectedItem);
                        listView.ItemsSource = null;
                        listView.ItemsSource = items;
                    }
                    else
                    {
                        Console.WriteLine("Fout bij het verwijderen van de waarneming.");
                    }
                }
            }
        }

        private async Task<bool> DeleteWaarnemingAsync(int wid)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://api.wiv.one/api/Waarnemingen/{wid}";
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {response.StatusCode} - {errorMessage}");
                    return false;
                }
            }
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = listView.SelectedItem as MyDataModel;
            if (selectedItem != null)
            {
                popupBeschrijving.Text = selectedItem.Toelichting;
                popupVoorkomen.Text = selectedItem.Voorkomen;
                popupDatumTijd.Text = selectedItem.DatumTijd;
                popupAantal.Text = selectedItem.Aantal.HasValue ? selectedItem.Aantal.Value.ToString() : "N/A";
                observationPopup.IsOpen = true;
            }
        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {
            observationPopup.IsOpen = false;
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
        public string Voorkomen { get; set; } // Voeg dit veld toe

        public string DatumTijd => $"{Datum:yyyy/MM/dd} {Tijd:hh\\:mm\\:ss}";
    }

    public class SoortDataModel
    {
        public int Sid { get; set; }
        public string Soort { get; set; }
        public string Voorkomen { get; set; }
    }
}




