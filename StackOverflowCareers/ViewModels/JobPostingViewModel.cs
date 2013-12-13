using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StackOverflowCareers.Annotations;
using StackOverflowCareers.Model;

namespace StackOverflowCareers.ViewModels
{
    public class JobPostingViewModel : INotifyPropertyChanged
    {

        public JobPostingViewModel(JobPosting jobPosting)
        {
            if(jobPosting == null)
                throw new ArgumentNullException("jobPosting");

            _jobPosting = jobPosting;
            ScrapeThatScreen(jobPosting.Id);
        }

        public async Task ScrapeThatScreen(string postUrl)
        {
            if (!string.IsNullOrWhiteSpace(postUrl))
            {
                var request = WebRequest.Create(new Uri(postUrl)) as HttpWebRequest;
                try
                {
                    var result = request.GetResponseAsync();
                    await result;

                    System.IO.Stream responseStream = result.Result.GetResponseStream();
                    string data;
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        data = reader.ReadToEnd();
                    }
                    responseStream.Close();
                    this.ProcessPostingResults(data);
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }


        private void ProcessPostingResults(string result)
        {
            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(result);
            this.JobPosting = _jobPosting.UpdateFromScreenScraper(document);
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
                OnPropertyChanged("JobPosting");
            }
        }


        public static string GetText(HtmlDocument document, string elementType,string className)
        {
            if (document != null)
            {
                var firstOrDefault = document.DocumentNode.Descendants()
                    .FirstOrDefault(node => node.GetAttributeValue(elementType, string.Empty).Contains(className));
                if (firstOrDefault != null)
                    return firstOrDefault.InnerText.Trim();
            }
            return "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
