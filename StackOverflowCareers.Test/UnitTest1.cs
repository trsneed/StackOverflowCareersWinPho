using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace StackOverflowCareers.Test
{
    [TestClass]
    public class UnitTest1
    {
        public readonly string heh = "http://careers.stackoverflow.com/jobs/45103/designer-intego-inc";
        [TestMethod]
        public void TestMethod1()
        {
         WebClient client = new WebClient();
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
