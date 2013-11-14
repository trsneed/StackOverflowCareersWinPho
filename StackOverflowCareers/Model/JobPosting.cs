using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowCareers.Model
{
    public class JobPosting
    {
        public JobPosting()
        {
            Categories = new List<string>();
        }
        public string  Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public List<string> Categories { get; set; }
    }
}
