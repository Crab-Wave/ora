using System.Net.Http;
using ORA.API.Http;
using unirest_net.http;
using HttpClient = ORA.API.Http.HttpClient;

namespace ORA.Core.Http
{
    public class UnirestHttpClient : HttpClient
    {
        public UnirestHttpClient(string baseUrl) : base(baseUrl) { }

        public UnirestHttpClient() { }

        public override HttpResponse Get(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.get(this._baseUrl + path).headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public override HttpResponse Post(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.post(this._baseUrl + path).body(request.Body)
                .headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public override HttpResponse Delete(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.delete(this._baseUrl + path).body(request.Body)
                .headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public override HttpResponse Put(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.put(this._baseUrl + path).body(request.Body)
                .headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }
    }
}
