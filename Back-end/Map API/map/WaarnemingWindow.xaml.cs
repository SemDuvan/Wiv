using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace map
{
    public partial class WaarnemingWindow : Window
    {
        private DateTime currentDateTime;
        private double latitude;
        private double longitude;
        private WaarnemingHandler waarnemingHandler;

        public WaarnemingWindow()
        {
            InitializeComponent();
            currentDateTime = DateTime.Now;
            this.Loaded += WaarnemingWindow_Loaded;

            var waarnemingService = new WaarnemingService();
            var soortService = new SoortService();
            var wetenschappelijkeNaamService = new WetenschappelijkeNaamService();
            waarnemingHandler = new WaarnemingHandler(waarnemingService, soortService, wetenschappelijkeNaamService);
        }

        private async void WaarnemingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var locationService = new LocationService();
            try
            {
                var (lat, lon) = await locationService.GetLocationAsync();
                latitude = lat;
                longitude = lon;
                Console.WriteLine($"Location obtained: Latitude = {latitude}, Longitude = {longitude}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kon locatie niet ophalen: {ex.Message}", "Fout");
                Console.WriteLine($"Kon locatie niet ophalen: {ex.Message}");
            }
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

        private void AantalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        private async void Verzend_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.IsEnabled = false;

            try
            {
                string name = nameTextBox.Text;
                string soort = soortTextBox.Text;
                bool zeldzaam = zeldzaamCheckBox.IsChecked ?? false;
                string description = descriptionTextBox.Text;
                DateTime datum = currentDateTime;
                TimeSpan tijd = currentDateTime.TimeOfDay;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(soort) || string.IsNullOrWhiteSpace(aantalTextBox.Text))
                {
                    MessageBox.Show("Naam, Soort, en Aantal zijn verplichte velden.", "Fout");
                    button.IsEnabled = true;
                    return;
                }

                if (!int.TryParse(aantalTextBox.Text, out int aantal))
                {
                    MessageBox.Show("Aantal moet een geldig getal zijn.", "Fout");
                    button.IsEnabled = true;
                    return;
                }

                await waarnemingHandler.VerzendWaarneming(name, soort, zeldzaam, description, datum, tijd, latitude, longitude, aantal);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout");
                Console.WriteLine($"Er is een fout opgetreden: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                button.IsEnabled = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Index Index = new Index();
            Index.Show();
            this.Close();
        }
    }
}
