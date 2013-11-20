using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackOverflowCareers.Model;

namespace StackOverflowCareers.Core
{
    public class LocationService : IDisposable
    {
        private GeoCoordinateWatcher _watcher;
        public EventHandler<bool> WatcherReadyEventHandler = delegate { };
        public EventHandler<string> LocationTextEventHandler = delegate { };
        private const string Locationserviceurl = "http://maps.googleapis.com/maps/api/geocode/json?latlng=";

        public LocationService(GeoCoordinateWatcher watcher)
        {
            _watcher = watcher;
            watcher.StatusChanged += watcher_StatusChanged;
            _watcher.Start();
        }

        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled: 
                    break;

                case GeoPositionStatus.Initializing:
                    break;

                case GeoPositionStatus.NoData:
                    break;

                case GeoPositionStatus.Ready:
                    this.WatcherReadyEventHandler(this, true);
                    break;
            }
        }

        public void LocateThePhone()
        {
            var myPoint = _watcher.Position;
            var url = string.Format("{0}{1},{2}&sensor=true", Locationserviceurl, myPoint.Location.Latitude, myPoint.Location.Longitude);
            var result = new WebClient();
            result.DownloadStringCompleted += (sender, args) =>
            {
               this.LocationTextEventHandler(this, ProcessLocationResponse(args.Result.ToString()));
            };
            result.DownloadStringAsync(new Uri(url));


        }

        private string ProcessLocationResponse(string args)
        {
            var locRes = JsonConvert.DeserializeObject<LocationServiceResults>(args);
            return locRes.results[3].formatted_address;
        }

        public void Dispose()
        {
            _watcher.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
