using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace StackOverflowCareers
{
    public static class Extensions
    {
        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var taskComplete = new TaskCompletionSource<HttpWebResponse>();
            request.BeginGetResponse(asyncResponse =>
            {
                try
                {
                    var responseRequest = (HttpWebRequest) asyncResponse.AsyncState;
                    var someResponse = (HttpWebResponse) responseRequest.EndGetResponse(asyncResponse);
                    taskComplete.TrySetResult(someResponse);
                }
                catch (WebException webExc)
                {
                    var failedResponse = (HttpWebResponse) webExc.Response;
                    taskComplete.TrySetResult(failedResponse);
                }
            }, request);
            return taskComplete.Task;
        }

        public static string CleanTheText(this string text)
        {
            return text.Replace("&nbsp;", " ");
        }

        public static string GetText(this HtmlDocument document, string elementType, string className)
        {
            if (document != null)
            {
                HtmlNode firstOrDefault = document.GetHtmlNode(elementType, className);

                if (firstOrDefault != null)
                    return firstOrDefault.InnerText.Trim();
            }
            return "";
        }

        public static string GetLink(this HtmlDocument document, string elementType, string className)
        {
            if (document != null)
            {
                HtmlNode firstOrDefault = document.GetHtmlNode(elementType, className);
                if (firstOrDefault != null)
                {
                    return firstOrDefault.Attributes["href"].Value;
                }
            }
            return "";
        }

        private static HtmlNode GetHtmlNode(this HtmlDocument document, string elementType, string className)
        {
            return document.DocumentNode.Descendants()
                .FirstOrDefault(node => node.GetAttributeValue(elementType, string.Empty).Contains(className));
        }

        public static IEnumerable<KeyValuePair<string, bool>> GetJoelTest(this HtmlDocument document, string elementType,
            string className)
        {
            var joelList = new List<KeyValuePair<string, bool>>();
            if (document != null)
            {
                HtmlNode node = document.GetHtmlNode(elementType, className);
                if (node != null)
                    joelList.AddRange(
                        node.Descendants()
                            .Where(n => n.Name == "li")
                            .Select(
                                source =>
                                    new KeyValuePair<string, bool>(source.InnerText,
                                        source.Attributes["class"] != null)));
            }
            return joelList;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
        {
            return new ObservableCollection<T>(_LinqResult);
        }
    }
}