using System;
using System.IO;
using CommandDotNet;
using CommandDotNet.FluentValidation;
using ORA.API;

namespace ORA.Application.CLI
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, "ora-tracker.txt");
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path).Trim();
                Uri uriResult;
                bool result = Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (result)
                    Ora.GetHttpClient().BaseUrl = text;
            }

            return new AppRunner<OraApplication>().UseFluentValidation().Run(args);
        }
    }
}
