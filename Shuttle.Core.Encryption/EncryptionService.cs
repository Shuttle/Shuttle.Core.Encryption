using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption;

public class EncryptionService : IEncryptionService
{
    private readonly Dictionary<string, IEncryptionAlgorithm> _encryptionAlgorithms = new();

    public EncryptionService(IEnumerable<IEncryptionAlgorithm>? encryptionAlgorithms = null)
    {
        foreach (var encryptionAlgorithm in encryptionAlgorithms ?? [])
        {
            Add(encryptionAlgorithm);
        }
    }

    public IEncryptionService Add(IEncryptionAlgorithm encryptionAlgorithm)
    {
        Guard.AgainstNull(encryptionAlgorithm);

        if (!_encryptionAlgorithms.TryAdd(encryptionAlgorithm.Name, encryptionAlgorithm))
        {
            throw new ArgumentException(string.Format(Resources.DuplicateEncryptionAlgorithmException, encryptionAlgorithm.Name));
        }

        return this;
    }

    public IEncryptionAlgorithm Get(string name)
    {
        Guard.AgainstEmpty(name);

        if (!_encryptionAlgorithms.TryGetValue(name, out var algorithm))
        {
            throw new ArgumentException(string.Format(Resources.EncryptionAlgorithmMissingException, name));
        }

        return algorithm;
    }

    public bool Contains(string name)
    {
        return _encryptionAlgorithms.ContainsKey(Guard.AgainstEmpty(name));
    }

    public IEnumerable<IEncryptionAlgorithm> Algorithms => _encryptionAlgorithms.Values;
}