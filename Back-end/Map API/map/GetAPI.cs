using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Wpf;

namespace map
{
    internal class GetAPI
    {
        private WebView2 webView;

        public GetAPI(WebView2 webView)
        {
            this.webView = webView;
        }

        public async Task InitializeMapAsync(string azureMapsKey)
        {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Settings.AreDevToolsEnabled = true; // Enable developer tools

            if (string.IsNullOrEmpty(azureMapsKey))
            {
                MessageBox.Show("Azure Maps key is not set in the configuration file.");
                return;
            }

            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "azuremap.html");
            string htmlContent = File.ReadAllText(htmlFilePath).Replace("{azureMapsKey}", azureMapsKey);
            webView.NavigateToString(htmlContent);
        }

        public void AddMarker(double latitude, double longitude)
        {
            string formattedLatitude = latitude.ToString(CultureInfo.InvariantCulture);
            string formattedLongitude = longitude.ToString(CultureInfo.InvariantCulture);

            string script = $"addMarker({formattedLatitude}, {formattedLongitude});";
            webView.ExecuteScriptAsync(script);
        }
    }
}

