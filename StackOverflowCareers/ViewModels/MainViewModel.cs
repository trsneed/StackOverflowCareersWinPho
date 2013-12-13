using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Microsoft.Phone.Tasks;
using StackOverflowCareers.Core;
using StackOverflowCareers.Model;
using StackOverflowCareers.Model.Criteria;
using StackOverflowCareers.Resources;

namespace StackOverflowCareers.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private LocationService _locationService;
        public const string SearchUrl = "http://careers.stackoverflow.com/jobs/feed?";

        public MainViewModel()
        {
            _locationService = new LocationService(new GeoCoordinateWatcher());
            _locationService.WatcherReadyEventHandler += WatcherReady;
            _locationService.LocationTextEventHandler += LocationTextEventHandler;
            this.JobPostings = new ObservableCollection<JobPosting>();
        }

        private void LocationTextEventHandler(object sender, string s)
        {
            WhereSearchText = s;
        }

        private void WatcherReady(object sender, bool b)
        {
            LocationServiceReady = b;
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        private ObservableCollection<JobPosting> _JobPostings;

        public ObservableCollection<JobPosting> JobPostings
        {
            get { return _JobPostings; }
            set
            {
                _JobPostings = value;
                OnPropertyChanged("JobPostings");
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        private bool _IsRemote;

        public bool IsRemote
        {
            get { return _IsRemote; }
            set
            {
                _IsRemote = value;
                OnPropertyChanged("IsRemote");
            }
        }

        private bool _IsRelocation;

        public bool IsRelocation
        {
            get { return _IsRelocation; }
            set
            {
                _IsRelocation = value;
                OnPropertyChanged("IsRelocation");
            }
        }

        private int _Distance = 10;

        public int Distance
        {
            get { return _Distance; }
            set
            {
                _Distance = value;
                OnPropertyChanged("Distance");
            }
        }

        private bool _IsSearchOpen;

        public bool IsSearchOpen
        {
            get { return _IsSearchOpen; }
            set
            {
                _IsSearchOpen = value;
                OnPropertyChanged("IsSearchOpen");
            }
        }

        private bool _LocationServiceReady;
        public bool LocationServiceReady
        {
            get { return _LocationServiceReady; }
            set
            {
                _LocationServiceReady = value;
                OnPropertyChanged("LocationServiceReady");
            }
        }

        private string _WhatSearchText;
        public string WhatSearchText
        {
            get { return _WhatSearchText; }
            set
            {
                _WhatSearchText = value;
                OnPropertyChanged("WhatSearchText");
            }
        }

        private string _WhereSearchText;
        public string WhereSearchText
        {
            get { return _WhereSearchText; }
            set
            {
                _WhereSearchText = value;
                OnPropertyChanged("WhereSearchText");
            }
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void UpdateFeedList(string feedXML)
        {
            
            // Load the feed into a SyndicationFeed instance.
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            var uncastItems = feed.Items;
            int i = 0;
            foreach (var syndicationItem in uncastItems.Take(20))
            {
                JobPostings.Add(new JobPosting().UpdatePostingFromRss(syndicationItem, i));
                i ++;
            }
        }        

        

        public async Task SearchCareers(SearchCriteria criteria = null)
        {
            IsLoading = true;
            LoadingText = "Searching Careers";
            JobPostings.Clear(); 
            var url = SearchUrl;
            if (criteria != null)
            {
                string criteriaString = criteria.Criteria.Aggregate("",
                    (current, searchParameter) =>
                        string.Format("{0}{1}={2}&", current, searchParameter.QueryString, searchParameter.QueryValue));
                url = string.Format("{0}{1}", url, criteriaString);
            }

            var request = HttpWebRequest.Create(new Uri(url)) as HttpWebRequest;
            try
            {
                var response = (HttpWebResponse) await request.GetResponseAsync();
                // Read the response into a Stream object.
                System.IO.Stream responseStream = response.GetResponseStream();
                string data;
                using (var reader = new System.IO.StreamReader(responseStream))
                {
                    data = reader.ReadToEnd();
                }
                responseStream.Close();
               UpdateFeedList(data);

                IsLoading = false;
            }
            catch (Exception)
            {
                MessageBox.Show("poop");
                throw;
            }

        }

        


        public SearchCriteria BuildCriteria()
        {
            var criteria = new SearchCriteria();
            if (!string.IsNullOrWhiteSpace(_WhatSearchText))
            {
                criteria.Criteria.Add(new KeyWordCriteria(_WhatSearchText));
            }
            if (!string.IsNullOrWhiteSpace(_WhereSearchText))
            {
                criteria.Criteria.Add(new LocationCriteria(_WhereSearchText));
                criteria.Criteria.Add(new DistanceCriteria(_Distance.ToString()));
                criteria.Criteria.Add(
                    new DistanceUnitsCriteria(RegionInfo.CurrentRegion.IsMetric
                        ? DistanecUnits.Kilometers
                        : DistanecUnits.Miles));
            }
            if (_IsRelocation)
            {
                criteria.Criteria.Add(new RelocationCriteria(_IsRelocation));
            }
            if (_IsRemote)
            {
                criteria.Criteria.Add(new RemoteCriteria(_IsRemote));
            }
            return criteria;
        }

        internal void GetLocation()
        {
            if (_LocationServiceReady)
            {
                _locationService.LocateThePhone();
            }
        }

        internal void RoundDistance(double sliderValue)
        {
            Distance = Convert.ToInt32(Math.Round((sliderValue / 10.0)) * 10);
        }
    }
}