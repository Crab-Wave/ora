using System;
using System.Collections.Generic;

namespace ORA.API.Http
{
    /// <summary>
    /// A HttpRequest is composed of two parts : a body and some headers
    /// </summary>
    public class HttpRequest
    {
        private string _body;
        private Dictionary<string, object> _headers;

        public string Body => this._body;
        public Dictionary<string, object> Headers => this._headers;

        /// <summary>
        /// Initializes a new HttpRequest.
        /// </summary>
        public HttpRequest()
        {
            this._body = "";
            this._headers = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new HttpRequest.
        /// </summary>
        /// <param name="body">the body of the new HttpRequest. </param>
        public HttpRequest(string body)
        {
            this._body = body;
            this._headers = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new HttpRequest.
        /// </summary>
        /// <param name="body">the body of the new HttpRequest. </param>
        /// <param name="headers">the headers of the new HttpRequest.</param>
        public HttpRequest(string body, Dictionary<string, object> headers)
        {
            this._body = body;
            this._headers = headers;
        }

        public HttpRequest Set(string key, string value)
        {
            this._headers.Add(key, value);
            return this;
        }

        /// <summary>
        /// Method to transform a HttpRequest in a long string
        /// </summary>
        /// <returns>the wanted string</returns>
        public override string ToString() => "HttpResponse{Body=" + this._body + ", Body=" + this._body + "}";
    }
}
