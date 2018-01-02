namespace Shuttle.Core.Encryption
{
    public interface IEncryptionAlgorithm
    {
        string Name { get; }

        byte[] Encrypt(byte[] bytes);
        byte[] Decrypt(byte[] bytes);
    }
}