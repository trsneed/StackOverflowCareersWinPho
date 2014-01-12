using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using StackOverflowCareers.Model;

namespace StackOverflowCareers.ViewModels
{
    public class JobPostingViewModel : BaseViewModel
    {
        private string _formattedCategories;

        private JobPosting _jobPosting;

        public JobPostingViewModel(JobPosting jobPosting)
        {
            if (jobPosting == null)
                throw new ArgumentNullException("jobPosting");

            JoelTestResults = new ObservableCollection<JoelTestResult>();
            // JoelTestResults = new ObservableCollection<string>(){"a","b","c"};
            _jobPosting = jobPosting;
            if (jobPosting.Categories != null && jobPosting.Categories.Any())
                ProcessCategories(jobPosting.Categories);
        }

        public string FormattedCategories
        {
            get { return _formattedCategories; }
            set
            {
                _formattedCategories = value;
                OnPropertyChanged("FormattedCategories");
            }
        }

        public JobPosting JobPosting
        {
            get { return _jobPosting; }
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

        public async Task ScrapeThatScreenAsync(string postUrl)
        {
            IsLoading = true;
            LoadingText = "Getting job information";
            if (!string.IsNullOrWhiteSpace(postUrl))
            {
                var request = WebRequest.Create(new Uri(postUrl)) as HttpWebRequest;
                try
                {
                    Task<HttpWebResponse> result = request.GetResponseAsync();
                    await Task.WhenAll(result);

                    Stream responseStream = result.Result.GetResponseStream();
                    string data;
                    using (var reader = new StreamReader(responseStream))
                    {
                        data = reader.ReadToEnd();
                    }
                    responseStream.Close();
                    ProcessPostingResults(data);
                }
                catch (Exception e)
                {
                    MessageBox.Show("poop");
                    throw;
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        private void ProcessCategories(IEnumerable<string> categories)
        {
            string formattedStuff = categories.Aggregate("", (current, category) => current + category + ", ");
            //remove the final comma and space
            FormattedCategories = formattedStuff.Remove(formattedStuff.Length - 2);
        }

        private void ProcessPostingResults(string result)
        {
            var document = new HtmlDocument();
            document.LoadHtml(result);
            JobPosting = _jobPosting.UpdateFromScreenScraper(document);
        }
    }
}