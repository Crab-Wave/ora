namespace ORA.API.Http
{
    /// <summary>
    /// A class to send HTTP request and receive HTTP response from endpoints of a base url.
    /// </summary>
    public abstract class HttpClient
    {
        protected string _baseUrl;

        public string BaseUrl
        {
            get => this._baseUrl;
            set => this._baseUrl = value;
        }

        protected HttpClient() : this("") { }

        protected HttpClient(string baseUrl)
        {
            this._baseUrl = baseUrl;
        }

        public HttpResponse Get(string path) => this.Get(path, new HttpRequest());

        public abstract HttpResponse Get(string path, HttpRequest request);

        public HttpResponse Post(string path) => this.Post(path, new HttpRequest());

        public abstract HttpResponse Post(string path, HttpRequest request);

        public HttpResponse Delete(string path) => this.Delete(path, new HttpRequest());

        public abstract HttpResponse Delete(string path, HttpRequest request);

        public HttpResponse Put(string path) => this.Put(path, new HttpRequest());

        public abstract HttpResponse Put(string path, HttpRequest request);
    }
}
