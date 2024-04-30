using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public static class EncryptionServiceExtensions
    {
        public static byte[] Encrypt(this IEncryptionService encryptionService, string name, byte[] bytes)
        {
            return Guard.AgainstNull(encryptionService, nameof(encryptionService)).Get(name).Encrypt(bytes);
        }

        public static async Task<byte[]> EncryptAsync(this IEncryptionService encryptionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(encryptionService, nameof(encryptionService)).Get(name).EncryptAsync(bytes);
        }

        public static byte[] Decrypt(this IEncryptionService encryptionService, string name, byte[] bytes)
        {
            return Guard.AgainstNull(encryptionService, nameof(encryptionService)).Get(name).Decrypt(bytes);
        }

        public static async Task<byte[]> DecryptAsync(this IEncryptionService encryptionService, string name, byte[] bytes)
        {
            return await Guard.AgainstNull(encryptionService, nameof(encryptionService)).Get(name).DecryptAsync(bytes);
        }
    }
}