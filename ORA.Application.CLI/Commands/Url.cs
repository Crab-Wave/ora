using System;
using System.IO;
using System.Net;
using CommandDotNet;
using ORA.API;

namespace ORA.Application.CLI.Commands
{
    [Command(Name = "url", Description = "Change the tracker URL", Usage = "url [url]")]
    public class Url
    {
        [DefaultMethod]
        public void DefaultCommand([Operand(Description = "Tracker URL")] string url)
        {
            if (String.IsNullOrWhiteSpace(url))
                Console.WriteLine("The current Tracker URL is " + Ora.GetHttpClient().GetBaseUrl());
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
                    Console.WriteLine("Tracker URL has been sucessfully reset to https://tracker.ora.crabwave.com");
                }
                else if (!result)
                    Console.WriteLine($"{url} is not a valid URL");
                else
                {
                    File.WriteAllText(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                            "ora-tracker.txt"), url);
                    Console.WriteLine($"Tracker URL has been sucessfully changed to {url}");
                }
            }
        }
    }
}