using System;
using System.Collections.Generic;
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

        public JobPostingViewModel(JobPosting post)
        {
            if(post == null)
                throw new ArgumentNullException("post");
            JobPosting = post;
            ScrapeThatScreen();
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
                if (value != _jobPosting)
                {
                    _jobPosting = value;
                    OnPropertyChanged("JobPosting");
                }
            }
        }

        public void ScrapeThatScreen()
        {
            if (!string.IsNullOrWhiteSpace(_jobPosting.Id))
            {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += (sender, args) => { ProcessPostingResults(args.Result.ToString()); };
                client.DownloadStringAsync(new Uri(_jobPosting.Id));
            }
        }


        private void ProcessPostingResults(string result)
        {
            var document = new HtmlDocument();
            document.LoadHtml(result);
            HtmlNode body = document.DocumentNode.Descendants().FirstOrDefault(n => n.Name == "body");
            //Let's see how this works, shall we.
            var inner = new HtmlDocument();
            inner.LoadHtml(body.InnerHtml);

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
