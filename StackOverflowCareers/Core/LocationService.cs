using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowCareers.Core
{
    public class LocationService
    {
        private GeoCoordinateWatcher _watcher;

        public LocationService(GeoCoordinateWatcher watcher)
        {
            _watcher = watcher;
        }
        
    }
}
