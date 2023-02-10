using System.Collections.Generic;

namespace Shuttle.Core.Encryption
{
    public interface IEncryptionService
    {
        IEncryptionService Add(IEncryptionAlgorithm encryptionAlgorithm);
        IEncryptionAlgorithm Get(string name);
        bool Contains(string name);
        IEnumerable<IEncryptionAlgorithm> Algorithms { get; }
    }
}