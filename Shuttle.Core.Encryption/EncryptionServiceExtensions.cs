using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption;

public static class EncryptionServiceExtensions
{
    extension(IEncryptionService encryptionService)
    {
        public async Task<byte[]> DecryptAsync(string name, byte[] bytes)
        {
            return await Guard.AgainstNull(encryptionService).Get(name).DecryptAsync(bytes);
        }

        public async Task<byte[]> EncryptAsync(string name, byte[] bytes)
        {
            return await Guard.AgainstNull(encryptionService).Get(name).EncryptAsync(bytes);
        }
    }
}