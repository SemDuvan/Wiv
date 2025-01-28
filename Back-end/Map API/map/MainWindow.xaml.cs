using System;
using System.Configuration;
using System.Globalization;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace map
{
    public partial class MainWindow : Window
    {
        private LocationService locationService;
        private PlaatsMarker getAPI;

        public MainWindow()
        {
            InitializeComponent();
            locationService = new LocationService();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            getAPI = new PlaatsMarker(webView);
            string azureMapsKey = ConfigurationManager.AppSettings["AzureMapsKey"];
            await getAPI.InitializeMapAsync(azureMapsKey);

            // Haal de locatie op en voeg de marker toe zodra de kaart is geladen
            var location = await locationService.GetLocationAsync();
            if (location.Latitude != 0 && location.Longitude != 0)
            {
                getAPI.AddMarker(location.Latitude, location.Longitude);
            }
        }

        private async void marker_Click(object sender, RoutedEventArgs e)
        {
            var location = await locationService.GetLocationAsync();
            if (location.Latitude == 0 && location.Longitude == 0)
            {
                MessageBox.Show("Unknown latitude and longitude.");
            }
            else
            {
                MessageBox.Show($"Lat: {location.Latitude} Long: {location.Longitude}");
                getAPI.AddMarker(location.Latitude, location.Longitude);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Index indexWindow = new Index();
            indexWindow.Show();
            this.Close();
        }
    }
}

