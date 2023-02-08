using System.Threading.Tasks;

namespace Shuttle.Core.Encryption
{
    public class NullEncryptionAlgorithm : IEncryptionAlgorithm
    {
        public string Name => "null";

        public Task<byte[]> Encrypt(byte[] bytes)
        {
            return Task.FromResult(bytes);
        }

        public Task<byte[]> Decrypt(byte[] bytes)
        {
            return Task.FromResult(bytes);
        }
    }
}