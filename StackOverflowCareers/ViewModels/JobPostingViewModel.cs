﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using StackOverflowCareers.Annotations;
using StackOverflowCareers.Model;

namespace StackOverflowCareers.ViewModels
{
    public class JobPostingViewModel : BaseViewModel
    {

        public JobPostingViewModel(JobPosting jobPosting)
        {
            if(jobPosting == null)
                throw new ArgumentNullException("jobPosting");

            JoelTestResults = new ObservableCollection<JoelTestResult>();
           // JoelTestResults = new ObservableCollection<string>(){"a","b","c"};
            _jobPosting = jobPosting;
            ProcessCategories(jobPosting.Categories);
        }

        public async Task ScrapeThatScreenAsync(string postUrl)
        {
            this.IsLoading = true;
            this.LoadingText = "Getting job information";
            if (!string.IsNullOrWhiteSpace(postUrl))
            {
                var request = WebRequest.Create(new Uri(postUrl)) as HttpWebRequest;
                try
                {
                    var result = request.GetResponseAsync();
                    await Task.WhenAll(result);

                    System.IO.Stream responseStream = result.Result.GetResponseStream();
                    string data;
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        data = reader.ReadToEnd();
                    }
                    responseStream.Close();
                    this.ProcessPostingResults(data);
                }
                catch (Exception e)
                {
                    MessageBox.Show("poop");
                    throw;
                }
                finally
                {
                    this.IsLoading = false;
                }
            }
        }

        private void ProcessCategories(IEnumerable<string> categories)
        {
            var formattedStuff = categories.Aggregate("", (current, category) => current + category + ", ");
            //remove the final comma and space
            FormattedCategories = formattedStuff.Remove(formattedStuff.Length - 2);
        }

        private void ProcessPostingResults(string result)
        {
            var document = new HtmlDocument();
            document.LoadHtml(result);
            this.JobPosting = _jobPosting.UpdateFromScreenScraper(document);
        }

        private string _formattedCategories;

        public string FormattedCategories
        {
            get { return _formattedCategories; }
            set
            {
                _formattedCategories = value;
                OnPropertyChanged("FormattedCategories");
            }
        }
        private JobPosting _jobPosting;

        public JobPosting JobPosting
        {
            get
            {
                return _jobPosting;
            }
            set
            {
                _jobPosting = value;
                JoelTestResults.Clear();
                //foreach (var joelTestResult in value.SpolskyTest)
                //{
                //    JoelTestResults.Add(joelTestResult.Name);
                //}
                JoelTestResults = value.SpolskyTest.ToObservableCollection();
                OnPropertyChanged("JoelTestResults");
                OnPropertyChanged("JobPosting");
            }
        }

        public ObservableCollection<JoelTestResult> JoelTestResults { get; set; } 

    }
}
