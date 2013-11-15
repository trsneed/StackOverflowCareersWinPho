using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowCareers.Model.Criteria
{
    public class DistanceUnitsCriteria : ISearchParameter
    {
        public DistanceUnitsCriteria(string text)
        {
            QueryString = "distanceUnits";
            QueryValue = text;
        }
        public string QueryString { get; private set; }
        public string QueryValue { get; private set; }
    }
}
