namespace Shuttle.Core.Encryption
{
    public interface IEncryptionService
    {
        byte[] Encrypt(string name, byte[] bytes);
        byte[] Decrypt(string name, byte[] bytes);
        IEncryptionService Add(IEncryptionAlgorithm encryptionAlgorithm);
        IEncryptionAlgorithm Get(string name);
    }
}