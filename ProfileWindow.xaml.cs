using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Device.Location;
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

namespace map
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
            GetLocationEvent();
        }

        GeoCoordinateWatcher watcher;
        public void GetLocationEvent()
        {
            this.watcher = new GeoCoordinateWatcher();
            this.watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);
            bool started = this.watcher.TryStart(false, TimeSpan.FromMilliseconds(20000));
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            PrintPosition(e.Position.Location.Latitude, e.Position.Location.Longitude);
        }

        void PrintPosition(double Latitude, double Longitude)
        {
            MessageBox.Show("latitude: " + Latitude + " longitude: " + Longitude);
            txtLatitude.Text = Latitude.ToString();
            txtLongitude.Text = Longitude.ToString();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"constr");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[LocationTable]([Id],[Latitude],[Longitude]) VALUES(@Id, @Latitude, @Longitude)", con);
            cmd.Parameters.AddWithValue("@Id", int.Parse(txtId.Text));
            cmd.Parameters.AddWithValue("@Latitude", float.Parse(txtLatitude.Text));
            cmd.Parameters.AddWithValue("@Longitude", float.Parse(txtLongitude.Text));
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data created successfully");
            con.Close();
        }
    }
}
