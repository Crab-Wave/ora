using JKang.IpcServiceFramework;
using ORA.API.Compression;

namespace ORA.Core.IPC.Compression
{
    public class IpcCompressor : ICompressor
    {
        private IpcServiceClient<ICompressor> _client;

        public IpcCompressor(IpcServiceClient<ICompressor> client)
        {
            this._client = client;
        }

        public byte[] Compress(byte[] data, int level) =>
            this._client.InvokeAsync(compressor => compressor.Compress(data, level)).Result;

        public byte[] Decompress(byte[] data) =>
            this._client.InvokeAsync(compressor => compressor.Decompress(data)).Result;
    }
}
