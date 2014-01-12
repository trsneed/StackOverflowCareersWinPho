namespace StackOverflowCareers.Model.Criteria
{
    public class DistanceCriteria : ISearchParameter
    {
        public DistanceCriteria(string text)
        {
            QueryString = "range";
            QueryValue = text;
        }

        public string QueryString { get; private set; }
        public string QueryValue { get; private set; }
    }
}