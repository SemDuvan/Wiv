using System;
using System.Device.Location;
using System.Threading.Tasks;

namespace map
{
    public class LocationService
    {
        public async Task<(double Latitude, double Longitude)> GetLocationAsync()
        {
            var tcs = new TaskCompletionSource<GeoCoordinate>();
            var watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.MovementThreshold = 1.0; // Gebruik een drempel van 1 meter voor updates

            watcher.StatusChanged += (s, e) =>
            {
                if (e.Status == GeoPositionStatus.Ready && !watcher.Position.Location.IsUnknown)
                {
                    tcs.TrySetResult(watcher.Position.Location);
                    watcher.Stop();
                }
            };

            watcher.Start();

            var coord = await tcs.Task.ConfigureAwait(false);
            return (coord.Latitude, coord.Longitude);
        }
    }
}

