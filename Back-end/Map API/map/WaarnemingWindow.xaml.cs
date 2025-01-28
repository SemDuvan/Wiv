using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace map
{
    public partial class WaarnemingWindow : Window
    {
        private string selectedCategory;
        private DateTime currentDateTime;

        public WaarnemingWindow()
        {
            InitializeComponent();
            currentDateTime = DateTime.Now;
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void Flora_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Flora";
            UpdateCategoryButtonStyles();
        }

        private void Fauna_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Fauna";
            UpdateCategoryButtonStyles();
        }

        private void UpdateCategoryButtonStyles()
        {
            var defaultColor = (Brush)new BrushConverter().ConvertFrom("#53c009"); // Groen
            var selectedColor = Brushes.Gray;

            FloraButton.Background = selectedCategory == "Flora" ? selectedColor : defaultColor;
            FaunaButton.Background = selectedCategory == "Fauna" ? selectedColor : defaultColor;
        }

        private async void Verzend_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.IsEnabled = false;

            try
            {
                string name = nameTextBox.Text;
                string description = descriptionTextBox.Text;
                DateTime datum = currentDateTime;
                TimeSpan tijd = currentDateTime.TimeOfDay;

                var data = new PushData
                {
                    Omschrijving = name,
                    Toelichting = description,
                    Datum = datum,
                    Tijd = tijd,
                    Sid = null, // Optioneel veld
                    WNid = null, // Optioneel veld
                    Lid = null, // Optioneel veld
                    Aantal = null, // Optioneel veld
                    Geslacht = null, // Optioneel veld
                    Gebruiker = null, // Optioneel veld
                    Zekerheid = null, // Optioneel veld
                    Webid = null, // Optioneel veld
                    ManierDelen = null // Optioneel veld
                };

                await PostDataToApiAsync(data);

                MessageBox.Show($"{selectedCategory} {name} {description}\nDatum en tijd: {datum:g}", "Invoer");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout");
            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        private async Task PostDataToApiAsync(PushData data)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://api.wiv.one/api/Waarnemingen";
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {response.StatusCode} - {errorMessage}");
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Index Index = new Index();
            Index.Show();
            this.Close();
        }
    }

    public class PushData
    {
        public int Wid { get; set; }
        public string Omschrijving { get; set; }
        public int? Sid { get; set; } = null; // Optioneel veld
        public DateTime Datum { get; set; }
        public TimeSpan Tijd { get; set; }
        public int? WNid { get; set; } = null; // Optioneel veld
        public int? Lid { get; set; } = null; // Optioneel veld
        public string Toelichting { get; set; }
        public int? Aantal { get; set; } = null; // Optioneel veld
        public string Geslacht { get; set; } = null; // Optioneel veld
        public string Gebruiker { get; set; } = null; // Optioneel veld
        public string Zekerheid { get; set; } = null; // Optioneel veld
        public int? Webid { get; set; } = null; // Optioneel veld
        public string ManierDelen { get; set; } = null; // Optioneel veld
    }
}
