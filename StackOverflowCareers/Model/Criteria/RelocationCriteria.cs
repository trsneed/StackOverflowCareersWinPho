using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowCareers.Model.Criteria
{
    public class RelocationCriteria: ISearchParameter
    {
        public RelocationCriteria(bool flag)
        {
            QueryString = "offersrelocation";
            QueryValue = flag ? "true" : "false";
        }
        public string QueryString { get; private set; }
        public string QueryValue { get; private set; }
    }
}
