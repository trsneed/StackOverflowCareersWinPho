namespace StackOverflowCareers.Model.Criteria
{
    public class RelocationCriteria : ISearchParameter
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