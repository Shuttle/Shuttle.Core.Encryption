using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption;

public static class EncryptionServiceExtensions
{
    public static async Task<byte[]> DecryptAsync(this IEncryptionService encryptionService, string name, byte[] bytes)
    {
        return await Guard.AgainstNull(encryptionService).Get(name).DecryptAsync(bytes);
    }

    public static async Task<byte[]> EncryptAsync(this IEncryptionService encryptionService, string name, byte[] bytes)
    {
        return await Guard.AgainstNull(encryptionService).Get(name).EncryptAsync(bytes);
    }
}