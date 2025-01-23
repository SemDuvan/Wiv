using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace map
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
            LoadObservations();
        }

        private void LoadObservations()
        {
            // Dummy data for observations
            var observations = new List<Observation>
            {
                new Observation { Name = "Vos", Category = "Fauna", Description = "Rood", DateTime = DateTime.Now.ToString("g") },
                new Observation { Name = "Eik", Category = "Flora", Description = "Groot", DateTime = DateTime.Now.ToString("g") }
            };

            // Bind the data to the ListView
            listView.ItemsSource = observations;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var indexWindow = new Index();
            indexWindow.Show();
            this.Close();
        }
    }

    public class Observation
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }
    }
}

