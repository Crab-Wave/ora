using System.Collections.Generic;

namespace ORA.API.Http
{
    /// <summary>
    /// A HttpResponse has three parts : a body, a return code and some headers.
    /// </summary>
    public class HttpResponse
    {
        private string _body;
        private int _code;
        private Dictionary<string, string> _headers;

        public string Body => this._body;
        public int Code => this._code;
        public Dictionary<string, string> Headers => this._headers;

        /// <summary>
        /// Initializes a new HttpResponse.
        /// </summary>
        /// <param name="body">the body of the new HttpResponse. </param>
        /// <param name="code">the code of the new HttpResponse.</param>
        /// <param name="headers">the headers of the new HttpResponse.</param>
        public HttpResponse(string body, int code, Dictionary<string, string> headers)
        {
            this._body = body;
            this._code = code;
            this._headers = headers;
        }

        /// <summary>
        /// Method to transform a HttpResponse in a long string
        /// </summary>
        /// <returns>the wanted string</returns>
        public override string ToString() => "HttpResponse{Code=" + this._code + ", Body=" + this._body + ", Headers=" +
                                             this._headers + "}";
    }
}
