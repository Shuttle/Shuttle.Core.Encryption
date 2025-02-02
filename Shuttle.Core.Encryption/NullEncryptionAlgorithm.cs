using System.Threading.Tasks;

namespace Shuttle.Core.Encryption;

public class NullEncryptionAlgorithm : IEncryptionAlgorithm
{
    public string Name => "null";

    public async Task<byte[]> EncryptAsync(byte[] bytes)
    {
        return await Task.FromResult(bytes);
    }

    public async Task<byte[]> DecryptAsync(byte[] bytes)
    {
        return await Task.FromResult(bytes);
    }
}