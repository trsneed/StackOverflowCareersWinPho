using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
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
                    HttpWebRequest responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
                    HttpWebResponse someResponse = (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
                    taskComplete.TrySetResult(someResponse);
                }
                catch (WebException webExc)
                {
                    HttpWebResponse failedResponse = (HttpWebResponse)webExc.Response;
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
                var firstOrDefault = document.DocumentNode.Descendants()
                    .FirstOrDefault(node => node.GetAttributeValue(elementType, string.Empty).Contains(className));
                if (firstOrDefault != null)
                    return firstOrDefault.InnerText.Trim();
            }
            return "";
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
        {
            return new ObservableCollection<T>(_LinqResult);
        }
    }
}
