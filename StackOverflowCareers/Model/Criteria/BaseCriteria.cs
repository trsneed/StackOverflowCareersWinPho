using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowCareers.Model.Criteria
{
    public class BaseCriteria : ISearchParameter
    {
        public BaseCriteria(string text, string searchWord)
        {
            QueryValue = text;
            QueryString = searchWord;
        }
        public string QueryString { get; private set; }
        public string QueryValue { get; private set; }
    }
}
