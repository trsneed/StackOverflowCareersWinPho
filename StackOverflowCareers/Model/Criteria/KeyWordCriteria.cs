namespace StackOverflowCareers.Model.Criteria
{
    public class KeyWordCriteria : ISearchParameter
    {
        public KeyWordCriteria(string text)
        {
            QueryString = "searchTerm";
            QueryValue = text;
        }

        public string QueryString { get; private set; }
        public string QueryValue { get; private set; }
    }
}