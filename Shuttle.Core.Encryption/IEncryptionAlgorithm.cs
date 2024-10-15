using System.Threading.Tasks;

namespace Shuttle.Core.Encryption;

public interface IEncryptionAlgorithm
{
    string Name { get; }
    Task<byte[]> DecryptAsync(byte[] bytes);

    Task<byte[]> EncryptAsync(byte[] bytes);
}