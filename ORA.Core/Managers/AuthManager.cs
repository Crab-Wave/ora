using System;
using System.Text;
using ORA.API;
using ORA.API.Http;
using ORA.API.Managers;
using ORA.Core.Encryption;

namespace ORA.Core.Managers
{
    public class AuthManager : IAuthManager
    {
        private string _token;

        public string Authenticate()
        {
            Identity identity = Ora.GetIdentityManager().GetIdentity();
            HttpResponse response = Ora.GetHttpClient()
                .Post("/auth", new HttpRequest(Convert.ToBase64String(identity.PublicKey)));
            if (response.Code == 200)
            {
                RsaCipher rsaCipher = new RsaCipher(identity.PublicKey, identity.PrivateKey);
                this._token = Encoding.UTF8.GetString(rsaCipher.Decrypt(Convert.FromBase64String(response.Body)));
            }
            else
                Ora.GetLogger().Error(new Exception("Couldn't authenticate user"));

            return this._token;
        }

        public string GetToken() => this._token;

        public string RefreshToken()
        {
            Identity identity = Ora.GetIdentityManager().GetIdentity();
            HttpResponse response = Ora.GetHttpClient()
                .Post("/refreshtoken", new HttpRequest().Set("Authorization", "Bearer " + this.GetToken()));
            if (response.Code == 200)
            {
                RsaCipher rsaCipher = new RsaCipher(identity.PublicKey, identity.PrivateKey);
                this._token = Encoding.UTF8.GetString(rsaCipher.Decrypt(Convert.FromBase64String(response.Body)));
            }
            else
            {
                Ora.GetLogger().Error(new Exception("Couldn't refresh user token"));
                this.Authenticate();
            }

            return this._token;
        }

        public bool IsAuthenticated() => this._token != null;

        public void Disconnect() =>
            Ora.GetHttpClient().Delete("/disconnect",
                new HttpRequest().Set("Authorization", "Bearer " + Ora.GetAuthManager().GetToken()));
    }
}
