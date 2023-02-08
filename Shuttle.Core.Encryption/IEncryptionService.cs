namespace Shuttle.Core.Encryption
{
    public interface IEncryptionService
    {
        IEncryptionService Add(IEncryptionAlgorithm encryptionAlgorithm);
        IEncryptionAlgorithm Get(string name);
        bool Contains(string name);
    }
}