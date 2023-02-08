using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public static class EncryptionServiceExtensions
    {
        public static async Task<byte[]> Encrypt(this IEncryptionService encryptionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(encryptionService, nameof(encryptionService)).Get(name).Encrypt(bytes);
        }

        public static async Task<byte[]> Decrypt(this IEncryptionService encryptionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(encryptionService, nameof(encryptionService)).Get(name).Decrypt(bytes);
        }
    }
}