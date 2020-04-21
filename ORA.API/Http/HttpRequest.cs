using System;
using System.Collections.Generic;

namespace ORA.API.Http
{
    public class HttpRequest
    {
        /// <summary>
        /// A HttpRequest is composed of two parts : a body and some headers
        /// </summary>
        private string _body;
        private Dictionary<string, object> _headers;

        /// <summary>
        /// Those attributes have both a getter
        /// </summary>
        public string Body => this._body;
        public Dictionary<string, object> Headers => this._headers;

        public HttpRequest()
        {
            this._body = "";
            this._headers = new Dictionary<string, object>();
        }

        public HttpRequest(string body)
        {
            this._body = body;
            this._headers = new Dictionary<string, object>();
        }

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

        public override string ToString() => "HttpResponse{Body=" + this._body + ", Body=" + this._body + "}";
    }
}
