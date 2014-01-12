using System;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackOverflowCareers.Model;

namespace StackOverflowCareers.Core
{
    public class LocationService : IDisposable
    {
        private const string Locationserviceurl = "http://dev.virtualearth.net/REST/v1/Locations/";

        private const string LocationServiceExtras =
            "?o=json&key=Atlwhaus20qbK2vp8HizUMOxBjZzRvyRqimwbJzaQ-uqFJjLvqUtNwD53ohLknN0";

        private readonly GeoCoordinateWatcher _watcher;
        public EventHandler<string> LocationTextEventHandler = delegate { };
        public EventHandler<bool> WatcherReadyEventHandler = delegate { };

        public LocationService(GeoCoordinateWatcher watcher)
        {
            _watcher = watcher;
            watcher.StatusChanged += watcher_StatusChanged;
            _watcher.Start();
        }

        public void Dispose()
        {
            _watcher.Dispose();
            GC.SuppressFinalize(this);
        }

        private void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
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
                    WatcherReadyEventHandler(this, true);
                    break;
            }
        }

        public async Task LocateThePhone()
        {
            GeoPosition<GeoCoordinate> myPoint = _watcher.Position;

            string url = string.Format("{0}{1},{2}{3}", Locationserviceurl, myPoint.Location.Latitude,
                myPoint.Location.Longitude, LocationServiceExtras);

            var request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            HttpWebResponse response = await request.GetResponseAsync();
            string locRes;
            Stream responseStream = response.GetResponseStream();
            using (var reader = new StreamReader(responseStream))
            {
                locRes = reader.ReadToEnd();
            }
            LocationTextEventHandler(this, ProcessLocationResponse(locRes));
        }

        private string ProcessLocationResponse(string args)
        {
            var locRes = JsonConvert.DeserializeObject<LocationServiceResult>(args);
            return locRes.resourceSets.First().resources.First().address.locality;
        }
    }
}