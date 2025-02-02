using System.Collections.Generic;

namespace Shuttle.Core.Encryption;

public interface IEncryptionService
{
    IEnumerable<IEncryptionAlgorithm> Algorithms { get; }
    IEncryptionService Add(IEncryptionAlgorithm encryptionAlgorithm);
    bool Contains(string name);
    IEncryptionAlgorithm Get(string name);
}