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

        public WaarnemingWindow()
        {
            InitializeComponent();
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

        private void Verzend_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            string description = descriptionTextBox.Text;
            string dateTime = DateTime.Now.ToString("g"); // "g" format geeft een korte datum en tijd weer
            MessageBox.Show($"{selectedCategory} {name} {description}\nDatum en tijd: {dateTime}", "Checkcheck");
        }
    }
}

