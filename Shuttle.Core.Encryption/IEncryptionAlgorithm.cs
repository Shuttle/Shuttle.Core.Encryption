using System.Threading.Tasks;

namespace Shuttle.Core.Encryption
{
    public interface IEncryptionAlgorithm
    {
        string Name { get; }

        byte[] Encrypt(byte[] bytes);
        byte[] Decrypt(byte[] bytes);

        Task<byte[]> EncryptAsync(byte[] bytes);
        Task<byte[]> DecryptAsync(byte[] bytes);
    }
}