using System.Net.Http;
using ORA.API.Http;
using unirest_net.http;

namespace ORA.Core.Http
{
    public class UnirestHttpClient : IHttpClient
    {
        public HttpResponse Get(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.get(path).headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public HttpResponse Post(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.post(path).body(request.Body).headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public HttpResponse Delete(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.delete(path).body(request.Body).headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }

        public HttpResponse Put(string path, HttpRequest request)
        {
            HttpResponse<string> response = Unirest.put(path).body(request.Body).headers(request.Headers).asString();
            return new HttpResponse(response.Body, response.Code, response.Headers);
        }
    }
}
