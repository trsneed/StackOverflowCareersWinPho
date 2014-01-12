using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using HtmlAgilityPack;

namespace StackOverflowCareers.Model
{
    public class JobPosting
    {
        public JobPosting()
        {
            SpolskyTest = new List<JoelTestResult>();
        }

        public string Id { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Summary { get; private set; }
        public string Title { get; private set; }
        public List<string> Categories { get; private set; }
        public int OrderId { get; private set; }
        public string Qualifications { get; private set; }
        public string Company { get; private set; }
        public string CompanyDescription { get; private set; }
        public string CompanyWebsite { get; private set; }
        public List<JoelTestResult> SpolskyTest { get; private set; }
        public string ApplyUrl { get; private set; }
        public string JobLocation { get; private set; }

        public JobPosting UpdatePostingFromRss(SyndicationItem syndicationItem, int i)
        {
            Id = syndicationItem.Id;
            PublishDate = syndicationItem.PublishDate.LocalDateTime;
            Summary = SanitizeString(syndicationItem.Summary.Text);
            Title = syndicationItem.Title.Text;
            OrderId = i;
            Categories = new List<string>();
            foreach (SyndicationCategory cat in syndicationItem.Categories)
            {
                Categories.Add(cat.Name);
            }

            return this;
        }

        public JobPosting UpdateFromScreenScraper(HtmlDocument document)
        {
            Title = document.GetText("class", "title");
            Company = document.GetText("class", "employer");
            JobLocation = document.GetText("class", "location");
            CompanyWebsite = document.GetLink("class", "employer");
            SpolskyTest.Clear();
            foreach (var test in document.GetJoelTest("id", "joeltest"))
            {
                SpolskyTest.Add(new JoelTestResult(test));
            }
            ApplyUrl = Id;
            List<HtmlNode> jobInformation =
                document.DocumentNode.Descendants()
                    .Where(node => node.GetAttributeValue("class", string.Empty).Contains("description")).ToList();

            //this is very hacky, but I am tired.
            //TODO: Figure out how to do this without relying on array index.
            Summary = HttpUtility.HtmlDecode(jobInformation[0].InnerText.Replace("Job Description", "").Trim());
            if (jobInformation.Count > 1)
                Qualifications = HttpUtility.HtmlDecode(jobInformation[1].InnerText.Trim());
            if (jobInformation.Count > 2)
                CompanyDescription = HttpUtility.HtmlDecode(jobInformation[2].InnerText.Trim());

            return this;
        }

        private static string SanitizeString(string value)
        {
            //needed to clean the rss
            while (true)
            {
                if (value.Contains("<") && value.Contains(">"))
                {
                    int openGator = value.IndexOf('<');
                    int closeGator = value.IndexOf('>');
                    int countGator = (closeGator + 1) - openGator;
                    value = value.Remove(openGator, countGator);
                }
                else
                {
                    return value;
                }
            }
        }
    }
}