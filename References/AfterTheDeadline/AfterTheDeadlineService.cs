using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace AfterTheDeadline
{
    public class AfterTheDeadlineService
    {
        private const string AfterTheDeadlineServiceAddress = "http://service.afterthedeadline.com/";

        private const string CheckDocumentCommand = "http://service.afterthedeadline.com/checkDocument?key={0}{1}&data={2}";
        private const string CheckGrammarCommand = "http://service.afterthedeadline.com/checkGrammar?key={0}{1}&data={2}";
        private const string InfoCommand = "http://service.afterthedeadline.com/info.slp?text={0}";
        private const string StatsCommand = "http://service.afterthedeadline.com/stats?key={0}{1}&data={2}";

        private static string _applicationKey = null;
        private static string _userKey = null;

        public static void InitService(string applicationKey, string userKey)
        {
            _applicationKey = applicationKey;
            _userKey = userKey;
        }

        private static void CheckKeys()
        {
            if ((string.IsNullOrEmpty(_applicationKey)) || (string.IsNullOrEmpty(_userKey)))
            {
                throw new ApplicationException("Application key and user key must be initialized, see http://www.afterthedeadline.com/api.slp for more details.");
            }
        }

        private static string EncodeString(string data)
        { 
            data = Regex.Replace(data, @"(\u2018|\u2019)", "'");
            data = HttpUtility.UrlEncode(data);
            return data;
        }

        /// <summary>
        /// Checks a document and returns errors and suggestions
        /// </summary>
        /// <param name="data">The data to check</param>
        /// <returns>Enumerable of error objects</returns>
        public static IEnumerable<Error> CheckDocument(string data)
        {
            CheckKeys();

            var webClient = new WebClient();

            var encodedData = EncodeString(data);
            string result = webClient.DownloadString(new Uri(string.Format(CheckDocumentCommand, _applicationKey, _userKey, encodedData)));

            XElement resultXml = XElement.Parse(result);

            var messages = resultXml.Descendants("message");
            if (messages.Count() != 0)
            {
                throw new ApplicationException(messages.First().Value);
            }

            var errors = from errorXml in resultXml.Descendants("error")
                         select new Error()
                         {
                             String = errorXml.Element("string") != null ? errorXml.Element("string").Value : null,
                             Description = errorXml.Element("description") != null ? errorXml.Element("description").Value : null,
                             Precontext = errorXml.Element("precontext") != null ? errorXml.Element("precontext").Value : null,
                             Type = errorXml.Element("type") != null ? errorXml.Element("type").Value : null,
                             Url = errorXml.Element("url") != null ? errorXml.Element("url").Value : null,
                             Suggestions = errorXml.Element("suggestions") != null ? 
                                 from optionXml in errorXml.Element("suggestions").Descendants("option")
                                 select optionXml.Value : null
                         };

            return errors;
        }

        /// <summary>
        /// Checks a document (sans spelling) returns errors and suggestions
        /// </summary>
        /// <param name="data">The data to check</param>
        /// <returns>Enumerable of error objects</returns>
        public static IEnumerable<Error> CheckGrammar(string data)
        {
            CheckKeys();

            var webClient = new WebClient();

            var encodedData = EncodeString(data);
            string result = webClient.DownloadString(new Uri(string.Format(CheckGrammarCommand, _applicationKey, _userKey, encodedData)));

            XElement resultXml = XElement.Parse(result);

            var messages = resultXml.Descendants("message");
            if (messages.Count() != 0)
            {
                throw new ApplicationException(messages.First().Value);
            }

            var errors = from errorXml in resultXml.Descendants("error")
                         select new Error()
                         {
                             String = errorXml.Element("string") != null ? errorXml.Element("string").Value : null,
                             Description = errorXml.Element("description") != null ? errorXml.Element("description").Value : null,
                             Precontext = errorXml.Element("precontext") != null ? errorXml.Element("precontext").Value : null,
                             Type = errorXml.Element("type") != null ? errorXml.Element("type").Value : null,
                             Url = errorXml.Element("url") != null ? errorXml.Element("url").Value : null,
                             Suggestions = errorXml.Element("suggestions") != null ?
                                 from optionXml in errorXml.Element("suggestions").Descendants("option")
                                 select optionXml.Value : null
                         };

            return errors;
        }

        /// <summary>
        /// Returns HTML describing an error
        /// </summary>
        /// <param name="text">The text that triggered an error</param>
        /// <returns>The HTML response</returns>
        public static string Info(string text)
        {
            CheckKeys();

            var webClient = new WebClient();

            var encodedText = EncodeString(text);
            string result = webClient.DownloadString(new Uri(string.Format(InfoCommand, encodedText)));

            return result;
        }

        /// <summary>
        /// Returns statistics about the writing quality of a document
        /// </summary>
        /// <param name="data">The data to check</param>
        public static IEnumerable<Metric> Stats(string data)
        {
            CheckKeys();

            var webClient = new WebClient();

            var encodedData = EncodeString(data);
            string result = webClient.DownloadString(new Uri(string.Format(StatsCommand, _applicationKey, _userKey, encodedData)));

            XElement resultXml = XElement.Parse(result);

            var metrics = from metricXml in resultXml.Descendants("metric")
                          select new Metric()
                          {
                              Type = (MetricType)Enum.Parse(typeof(MetricType), metricXml.Element("type").Value, true),
                              Key = (MetricKey)Enum.Parse(typeof(MetricKey), metricXml.Element("key").Value.Replace(" ", ""), true),
                              Value = int.Parse(metricXml.Element("value").Value)
                          };

            return metrics;
        }
    }
}
