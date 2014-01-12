namespace StackOverflowCareers.Model.Criteria
{
    public class RemoteCriteria : ISearchParameter
    {
        public RemoteCriteria(bool flag)
        {
            QueryString = "allowsremote";
            QueryValue = flag ? "true" : "false";
        }

        public string QueryString { get; private set; }
        public string QueryValue { get; private set; }
    }
}