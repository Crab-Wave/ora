namespace ORA.API.Http
{
    public interface IHttpClient
    {
        HttpResponse Get(string path) => this.Get(path, new HttpRequest());

        HttpResponse Get(string path, HttpRequest request);

        HttpResponse Post(string path) => this.Post(path, new HttpRequest());

        HttpResponse Post(string path, HttpRequest request);

        HttpResponse Delete(string path) => this.Delete(path, new HttpRequest());

        HttpResponse Delete(string path, HttpRequest request);

        HttpResponse Put(string path) => this.Put(path, new HttpRequest());

        HttpResponse Put(string path, HttpRequest request);
    }
}
