using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace map
{
    public partial class WaarnemingWindow : Window
    {
        private string selectedCategory;
        private LocationService locationService;

        public WaarnemingWindow()
        {
            InitializeComponent();
            locationService = new LocationService();
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                uploadedImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void Flora_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Flora";
        }

        private void Fauna_Click(object sender, RoutedEventArgs e)
        {
            selectedCategory = "Fauna";
        }

        private async void Verzend_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string description = descriptionTextBox.Text;
            string dateTime = DateTime.Now.ToString("g"); // "g" format geeft een korte datum en tijd weer
            var location = await locationService.GetLocationAsync();
            if (location.Latitude == 0 && location.Longitude == 0)
            {
                MessageBox.Show($"{selectedCategory} {name} {description}\nDatum en tijd: {dateTime}\nLocatie onbekend", "Invoer");
            }
            else
            {
                MessageBox.Show($"{selectedCategory} {name} {description}\nDatum en tijd: {dateTime}\nLatitude: {location.Latitude}, Longitude: {location.Longitude}", "Invoer");
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

