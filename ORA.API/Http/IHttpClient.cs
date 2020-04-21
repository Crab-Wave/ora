namespace ORA.API.Http
{
    public interface IHttpClient
    {
        public void SetBaseUrl(string baseUrl);

        public string GetBaseUrl();

        public HttpResponse Get(string path) => this.Get(path, new HttpRequest());

        public HttpResponse Get(string path, HttpRequest request);

        public HttpResponse Post(string path) => this.Post(path, new HttpRequest());

        public HttpResponse Post(string path, HttpRequest request);

        public HttpResponse Delete(string path) => this.Delete(path, new HttpRequest());

        public HttpResponse Delete(string path, HttpRequest request);

        public HttpResponse Put(string path) => this.Put(path, new HttpRequest());

        public HttpResponse Put(string path, HttpRequest request);
    }
}
