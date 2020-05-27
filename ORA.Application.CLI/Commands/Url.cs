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
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps) &&
                              Ora.Get().IsOraTracker(url);
                if (url.ToLower().Equals("reset"))
                {
                    Ora.GetAuthManager().Disconnect();
                    string trackerPath = Ora.GetDirectory("ora-tracker");
                    System.IO.File.WriteAllText(trackerPath, "https://tracker.ora.crabwave.com");
                    Ora.GetHttpClient().SetBaseUrl("https://tracker.ora.crabwave.com");
                    Ora.GetAuthManager().Authenticate();
                    Ora.GetNodeManager().Initialize();
                    Console.WriteLine("Tracker URL has been sucessfully reset to https://tracker.ora.crabwave.com");
                }
                else if (!result)
                    Console.WriteLine($"{url} is not a valid URL");
                else
                {
                    Ora.GetAuthManager().Disconnect();
                    string trackerPath = Ora.GetDirectory("ora-tracker");
                    System.IO.File.WriteAllText(trackerPath, url);
                    Ora.GetHttpClient().SetBaseUrl(url);
                    Ora.GetAuthManager().Authenticate();
                    Ora.GetNodeManager().Initialize();
                    Console.WriteLine($"Tracker URL has been sucessfully changed to {url}");
                }
            }
        }
    }
}
