using System;
using System.IO;
using System.Net;
using CommandDotNet;
using ORA.API;

namespace ORA.Application.CLI
{
    [Command]
    public class OraApplication
    {
        [SubCommand]
        public Clusters Clusters
        {
            get;
            set;
        }

        public OraApplication()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path,"ora-tracker.txt");
            if (!File.Exists(path))
                return;

            string text = File.ReadAllText(path).Trim();
            Uri uriResult;
            bool result = Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (result)
                Ora.GetHttpClient().BaseUrl = text;
        }

        [Command(Description = "change the tracker url", Name = "url")]
        public void Url([Operand(Description = " ")] string url)
        {
            if (String.IsNullOrWhiteSpace(url))
                Console.WriteLine("The current url is " + Ora.GetHttpClient().BaseUrl);
            else
            {
                Uri uriResult;

                bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (!result)
                    Console.WriteLine($"{url} is not a valid url");
                else
                {
                    File.WriteAllText(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "ora-tracker.txt"), url);
                    Console.WriteLine("Url has been sucessfully changed");
                }
            }
        }
    }
}
