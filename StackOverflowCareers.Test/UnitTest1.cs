using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace StackOverflowCareers.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new WebClient();
            string res = "";
            client.DownloadStringCompleted += (sender, args) => { Result(args.Result.ToString());};
             client.DownloadStringAsync(new Uri("http://careers.stackoverflow.com/jobs/21823/surprisingly-simple-software-developer-wa-best-appature-inc"));
        }

        public void Result(string result)
        {
            
        }
    }

    public class CareerPosting
    {
        public string Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public List<string> Categories { get; set; }
    }
}
