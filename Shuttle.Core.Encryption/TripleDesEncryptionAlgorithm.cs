using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public class TripleDesEncryptionAlgorithm : IEncryptionAlgorithm
    {
        private readonly TripleDESCryptoServiceProvider _provider;

        public TripleDesEncryptionAlgorithm(IOptions<TripleDesOptions> settings)
        {
            Guard.AgainstNull(settings, nameof(settings));

            _provider = new TripleDESCryptoServiceProvider
            {
                IV = new byte[8],
                Key =
                    new PasswordDeriveBytes(settings.Value.Key, Array.Empty<byte>()).CryptDeriveKey("RC2", "MD5", 128,
                        new byte[8])
            };
        }

        public string Name => "3DES";

        public async Task<byte[]> Encrypt(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return await TripleDesEncrypt(bytes);
        }

        public async Task<byte[]> Decrypt(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return await TripleDesDecrypt(bytes);
        }

        private async Task<byte[]> TripleDesEncrypt(byte[] plain)
        {
            return await GetEncryptedBytes(plain.Length, plain);
        }

        private async Task<byte[]> GetEncryptedBytes(int plainLength, byte[] plainBytes)
        {
            byte[] encryptedBytes;

            using (var ms = new MemoryStream(plainLength * 2 - 1))
            {
                var cs = new CryptoStream(ms, _provider.CreateEncryptor(), CryptoStreamMode.Write);

                await using (cs.ConfigureAwait(false))
                {
                    await cs.WriteAsync(plainBytes, 0, plainBytes.Length).ConfigureAwait(false);

                    cs.FlushFinalBlock();

                    encryptedBytes = new byte[(int) ms.Length];

                    ms.Position = 0;

                    var _ = await ms.ReadAsync(encryptedBytes, 0, (int) ms.Length).ConfigureAwait(false);
                }
            }

            return encryptedBytes;
        }

        private Task<byte[]> TripleDesDecrypt(byte[] encrypted)
        {
            return GetPlainBytes(encrypted.Length, encrypted);
        }

        private async Task<byte[]> GetPlainBytes(int secureLength, byte[] encryptedBytes)
        {
            byte[] plainBytes;

            using (var ms = new MemoryStream(secureLength))
            {
                var cs = new CryptoStream(ms, _provider.CreateDecryptor(), CryptoStreamMode.Write);

                await using (cs.ConfigureAwait(false))
                {
                    await cs.WriteAsync(encryptedBytes, 0, encryptedBytes.Length).ConfigureAwait(false);

                    cs.FlushFinalBlock();

                    plainBytes = new byte[(int) ms.Length];

                    ms.Position = 0;

                    var _ = await ms.ReadAsync(plainBytes, 0, (int)ms.Length).ConfigureAwait(false);
                }
            }

            return plainBytes;
        }
    }
}