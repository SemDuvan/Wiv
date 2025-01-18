using System;
using System.Configuration;
using System.Device.Location;
using System.IO;
using System.Windows;
using Microsoft.Web.WebView2.Core;

namespace map
{
    public partial class MainWindow : Window
    {
        private GeoCoordinateWatcher watcher;
        private double latitude;
        private double longitude;

        public MainWindow()
        {
            InitializeComponent();
            InitializeWebView();
            GetLocationEvent();
        }

        private void GetLocationEvent()
        {
            watcher = new GeoCoordinateWatcher();
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            watcher.TryStart(false, TimeSpan.FromMilliseconds(20000));
        }

        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            latitude = e.Position.Location.Latitude;
            longitude = e.Position.Location.Longitude;
            Console.WriteLine($"Position changed: Latitude = {latitude}, Longitude = {longitude}");
        }

        private void marker_Click(object sender, RoutedEventArgs e)
        {
            if (watcher.Position.Location.IsUnknown)
            {
                MessageBox.Show("Unknown latitude and longitude.");
            }
            else
            {
                MessageBox.Show($"Lat: {latitude} Long: {longitude}");
                InjectCoordinatesIntoWebView(latitude, longitude);
            }
        }

        private async void InjectCoordinatesIntoWebView(double latitude, double longitude)
        {
            string script = $"addMarker({latitude}, {longitude});";
            Console.WriteLine($"Executing script: {script}");
            await webView.CoreWebView2.ExecuteScriptAsync(script);
        }

        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Settings.AreDevToolsEnabled = true; // Enable developer tools
            string azureMapsKey = ConfigurationManager.AppSettings["AzureMapsKey"];
            if (string.IsNullOrEmpty(azureMapsKey))
            {
                MessageBox.Show("Azure Maps key is not set in the configuration file.");
                return;
            }
            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "azuremap.html");
            string htmlContent = File.ReadAllText(htmlFilePath).Replace("{azureMapsKey}", azureMapsKey);
            webView.NavigateToString(htmlContent);
        }
    }
}










