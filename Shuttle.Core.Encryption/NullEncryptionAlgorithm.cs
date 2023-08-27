using System.Threading.Tasks;

namespace Shuttle.Core.Encryption
{
    public class NullEncryptionAlgorithm : IEncryptionAlgorithm
    {
        public string Name => "null";

        public byte[] Encrypt(byte[] bytes)
        {
            return bytes;
        }

        public byte[] Decrypt(byte[] bytes)
        {
            return bytes;
        }

        public Task<byte[]> EncryptAsync(byte[] bytes)
        {
            return Task.FromResult(bytes);
        }

        public Task<byte[]> DecryptAsync(byte[] bytes)
        {
            return Task.FromResult(bytes);
        }
    }
}