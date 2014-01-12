using System.Collections.Generic;
using StackOverflowCareers.Model.Criteria;

namespace StackOverflowCareers.Model
{
    public class SearchCriteria
    {
        public List<ISearchParameter> Criteria;

        public SearchCriteria()
        {
            Criteria = new List<ISearchParameter>();
        }
    }
}