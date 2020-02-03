using System.Collections.Generic;

namespace ORA.API.Http
{
    public class HttpResponse
    {
        private string _body;
        private int _code;
        private Dictionary<string, string> _headers;

        public string Body => this._body;
        public int Code => this._code;
        public Dictionary<string, string> Headers => this._headers;

        public HttpResponse(string body, int code, Dictionary<string, string> headers)
        {
            this._body = body;
            this._code = code;
            this._headers = headers;
        }

        public override string ToString() => "HttpResponse{Code=" + this._code + ", Body=" + this._body + ", Headers=" +
                                             this._headers + "}";
    }
}
