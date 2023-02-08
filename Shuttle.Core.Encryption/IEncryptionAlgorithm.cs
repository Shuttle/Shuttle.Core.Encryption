using System.Threading.Tasks;

namespace Shuttle.Core.Encryption
{
    public interface IEncryptionAlgorithm
    {
        string Name { get; }

        Task<byte[]> Encrypt(byte[] bytes);
        Task<byte[]> Decrypt(byte[] bytes);
    }
}