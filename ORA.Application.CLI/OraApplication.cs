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
                if (url.ToLower().Equals("reset"))
                {
                    File.WriteAllText(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "ora-tracker.txt"), "https://tracker.ora.crabwave.com");
                    Console.WriteLine("Url has been sucessfully reset to https://tracker.ora.crabwave.com");
                }
                else if (!result)
                    Console.WriteLine($"{url} is not a valid url");
                else
                {
                    File.WriteAllText(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "ora-tracker.txt"), url);
                    Console.WriteLine($"Url has been sucessfully changed to {url}");
                }
            }
        }
    }
}
