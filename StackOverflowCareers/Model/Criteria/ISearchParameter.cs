namespace StackOverflowCareers.Model.Criteria
{
    public interface ISearchParameter
    {
        string QueryString { get;  }
        string QueryValue { get; }
    }
}
