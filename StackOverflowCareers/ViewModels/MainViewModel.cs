using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Microsoft.Phone.Tasks;
using StackOverflowCareers.Model;
using StackOverflowCareers.Resources;

namespace StackOverflowCareers.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.JobPostings = new ObservableCollection<JobPosting>();

            // WebClient is used instead of HttpWebRequest in this code sample because 
            // the implementation is simpler and easier to use, and we do not need to use 
            // advanced functionality that HttpWebRequest provides, such as the ability to send headers.
            WebClient webClient = new WebClient();

            // Subscribe to the DownloadStringCompleted event prior to downloading the RSS feed.
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

            // Download the RSS feed. DownloadStringAsync was used instead of OpenStreamAsync because we do not need 
            // to leave a stream open, and we will not need to worry about closing the channel. 
            webClient.DownloadStringAsync(new System.Uri("http://careers.stackoverflow.com/jobs/feed?searchTerm=austin&allowsremote=True"));

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
                NotifyPropertyChanged("JobPositings");
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                NotifyPropertyChanged("IsLoading");

            }
        }

        private string _LoadingText;

        public string LoadingText
        {
            get
            {
                return _LoadingText;
            }
            set
            {
                _LoadingText = value;
                NotifyPropertyChanged("LoadingText");
            }
        }

        private bool _IsSearchOpen;

        public bool IsSearchOpen
        {
            get { return _IsSearchOpen; }
            set
            {
                _IsSearchOpen = value;
                NotifyPropertyChanged("IsSearchOpen");
            }
        }

        private string _WhatSearchText;

        public string WhatSearchText
        {
            get { return _WhatSearchText; }
            set
            {
                _WhatSearchText = value;
                NotifyPropertyChanged("WhatSearchText");
            }
        }

        private string _WhereSearchText;

        public string WhereSearchText
        {
            get { return _WhereSearchText; }
            set
            {
                _WhereSearchText = value;
                NotifyPropertyChanged("WhereSearchText");
            }
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            //this.Items.Add(new ItemViewModel() { ID = "0", LineOne = "runtime one", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            //this.Items.Add(new ItemViewModel() { ID = "1", LineOne = "runtime two", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            //this.Items.Add(new ItemViewModel() { ID = "2", LineOne = "runtime three", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            //this.Items.Add(new ItemViewModel() { ID = "3", LineOne = "runtime four", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            //this.Items.Add(new ItemViewModel() { ID = "4", LineOne = "runtime five", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            //this.Items.Add(new ItemViewModel() { ID = "5", LineOne = "runtime six", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            //this.Items.Add(new ItemViewModel() { ID = "6", LineOne = "runtime seven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            //this.Items.Add(new ItemViewModel() { ID = "7", LineOne = "runtime eight", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });
            //this.Items.Add(new ItemViewModel() { ID = "8", LineOne = "runtime nine", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu" });
            //this.Items.Add(new ItemViewModel() { ID = "9", LineOne = "runtime ten", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus" });
            //this.Items.Add(new ItemViewModel() { ID = "10", LineOne = "runtime eleven", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Habitant inceptos interdum lobortis nascetur pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent" });
            //this.Items.Add(new ItemViewModel() { ID = "11", LineOne = "runtime twelve", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Ultrices vehicula volutpat maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos" });
            //this.Items.Add(new ItemViewModel() { ID = "12", LineOne = "runtime thirteen", LineTwo = "Maecenas praesent accumsan bibendum", LineThree = "Maecenas praesent accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur" });
            //this.Items.Add(new ItemViewModel() { ID = "13", LineOne = "runtime fourteen", LineTwo = "Dictumst eleifend facilisi faucibus", LineThree = "Pharetra placerat pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent" });
            //this.Items.Add(new ItemViewModel() { ID = "14", LineOne = "runtime fifteen", LineTwo = "Habitant inceptos interdum lobortis", LineThree = "Accumsan bibendum dictumst eleifend facilisi faucibus habitant inceptos interdum lobortis nascetur pharetra placerat" });
            //this.Items.Add(new ItemViewModel() { ID = "15", LineOne = "runtime sixteen", LineTwo = "Nascetur pharetra placerat pulvinar", LineThree = "Pulvinar sagittis senectus sociosqu suscipit torquent ultrices vehicula volutpat maecenas praesent accumsan bibendum" });

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Event handler which runs after the feed is fully downloaded.
        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    // Showing the exact error message is useful for debugging. In a finalized application, 
                    // output a friendly and applicable string to the user instead. 
                    MessageBox.Show(e.Error.Message);
                });
            }
            else
            {
                UpdateFeedList(e.Result);
            }
        }

        private void UpdateFeedList(string feedXML)
        {
            // Load the feed into a SyndicationFeed instance.
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            var uncastItems = feed.Items;
            int i = 0;
            foreach (var syndicationItem in uncastItems)
            {
                var job = new JobPosting();
                job.Id = syndicationItem.Id;
                job.PublishDate = syndicationItem.PublishDate.LocalDateTime;
                job.Summary = SanitizeString(syndicationItem.Summary.Text);
                job.Title = syndicationItem.Title.Text;
                job.OrderId = i;
                foreach (var cat in syndicationItem.Categories)
                {
                    job.Categories.Add(cat.Name);
                }

                JobPostings.Add(job);
                i ++;
            }
        }

        private static string SanitizeString(string value)
        {
            while (true)
            {
                if (value.Contains("<") && value.Contains(">"))
                {
                    var openGator = value.IndexOf('<');
                    var closeGator = value.IndexOf('>');
                    var countGator = (closeGator + 1) - openGator;
                    value = value.Remove(openGator, countGator);
                }
                else
                {
                    return value;
                }
            }
        }

        private async Task SearchCareers()
        {
            
        }
    }
}