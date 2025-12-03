namespace Shuttle.Core.Encryption;

public interface IEncryptionAlgorithm
{
    string Name { get; }
    Task<byte[]> DecryptAsync(byte[] bytes, CancellationToken cancellationToken = default);

    Task<byte[]> EncryptAsync(byte[] bytes, CancellationToken cancellationToken = default);
}