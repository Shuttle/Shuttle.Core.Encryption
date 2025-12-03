
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption;

public class TripleDesEncryptionAlgorithm : IEncryptionAlgorithm
{
    private readonly TripleDES _provider;

    public TripleDesEncryptionAlgorithm(IOptions<TripleDesOptions> options)
    {
        Guard.AgainstNull(options);

        _provider = TripleDES.Create();
        var iv = new byte[8];
        var key = new byte[16];

        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(options.Value.Key));
            Array.Copy(hash, key, 16);
        }

        _provider.Key = key;

        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(options.Value.Key));
            Array.Copy(hash, iv, 8);
        }

        _provider.IV = iv;
    }

    public string Name => "3DES";

    public async Task<byte[]> EncryptAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(bytes);

        using var ms = new MemoryStream(bytes.Length * 2 - 1);
        await using var cs = new CryptoStream(ms, _provider.CreateEncryptor(), CryptoStreamMode.Write);

        await cs.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);

        await cs.FlushFinalBlockAsync(cancellationToken);

        var encryptedBytes = new byte[(int)ms.Length];

        ms.Position = 0;

        _ = await ms.ReadAsync(encryptedBytes, 0, (int)ms.Length, cancellationToken).ConfigureAwait(false);

        return encryptedBytes;
    }

    public async Task<byte[]> DecryptAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(bytes);

        using var ms = new MemoryStream(bytes.Length);
        await using var cs = new CryptoStream(ms, _provider.CreateDecryptor(), CryptoStreamMode.Write);

        await cs.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);

        await cs.FlushFinalBlockAsync(cancellationToken);

        var plainBytes = new byte[(int)ms.Length];

        ms.Position = 0;

        _ = await ms.ReadAsync(plainBytes, 0, (int)ms.Length, cancellationToken).ConfigureAwait(false);

        return plainBytes;
    }
}