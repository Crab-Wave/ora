using JKang.IpcServiceFramework;
using ORA.API.Encryption;

namespace ORA.Core.IPC.Encryption
{
    public class IpcCipher : ICipher
    {
        private IpcServiceClient<ICipher> _client;

        public IpcCipher(IpcServiceClient<ICipher> client)
        {
            this._client = client;
        }

        public byte[] Encrypt(byte[] data) => this._client.InvokeAsync(cipher => cipher.Encrypt(data)).Result;

        public byte[] Decrypt(byte[] data) => this._client.InvokeAsync(cipher => cipher.Decrypt(data)).Result;
    }
}
