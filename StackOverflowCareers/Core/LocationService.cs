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
        private const string Locationserviceurl = "http://dev.virtualearth.net/REST/v1/Locations/";

        private const string LocationServiceExtras =
            "?o=json&key=Atlwhaus20qbK2vp8HizUMOxBjZzRvyRqimwbJzaQ-uqFJjLvqUtNwD53ohLknN0";
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

        public async Task LocateThePhone()
        {
            var myPoint = _watcher.Position;

            var url = string.Format("{0}{1},{2}{3}", Locationserviceurl, myPoint.Location.Latitude,
                myPoint.Location.Longitude, LocationServiceExtras);

            var request = HttpWebRequest.Create(new Uri(url)) as HttpWebRequest;
            var response = (HttpWebResponse) await request.GetResponseAsync();
            string locRes;
            System.IO.Stream responseStream = response.GetResponseStream();
            using (var reader = new System.IO.StreamReader(responseStream))
            {
                locRes = reader.ReadToEnd();
            }
            this.LocationTextEventHandler(this, ProcessLocationResponse(locRes));
        }

        private string ProcessLocationResponse(string args)
        {
            var locRes = JsonConvert.DeserializeObject<LocationServiceResult>(args);
            return locRes.resourceSets.First().resources.First().address.locality;
        }

        public void Dispose()
        {
            _watcher.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
