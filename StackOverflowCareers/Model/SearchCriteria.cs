using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowCareers.Model.Criteria;

namespace StackOverflowCareers.Model
{
    public class SearchCriteria
    {
        public SearchCriteria()
        {
            Criteria = new List<ISearchParameter>();
        }
        public List<ISearchParameter> Criteria;
    }
}
