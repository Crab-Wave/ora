using ORA.API.Http;
using unirest_net.http;

namespace ORA.Core.Http
{
    public class UnirestHttpClient : IHttpClient
    {
        private string _baseUrl;

        public UnirestHttpClient(string baseUrl)
        {
            this._baseUrl = baseUrl;
        }

        public UnirestHttpClient()
        {
            this._baseUrl = "";
        }

        public void SetBaseUrl(string baseUrl) => this._baseUrl = baseUrl;

        public string GetBaseUrl() => this._baseUrl;

        public HttpResponse Get(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.get(this._baseUrl + path).headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public HttpResponse Post(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.post(this._baseUrl + path).body(request.Body)
                .headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public HttpResponse Delete(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.delete(this._baseUrl + path).body(request.Body)
                .headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public HttpResponse Put(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.put(this._baseUrl + path).body(request.Body)
                .headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }
    }
}
