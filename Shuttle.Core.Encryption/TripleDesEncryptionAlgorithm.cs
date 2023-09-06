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

        public byte[] Encrypt(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return TripleDesEncryptAsync(bytes, true).GetAwaiter().GetResult();
        }

        public async Task<byte[]> EncryptAsync(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return await TripleDesEncryptAsync(bytes, false);
        }

        public byte[] Decrypt(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return TripleDesDecrypt(bytes);
        }

        public async Task<byte[]> DecryptAsync(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return await TripleDesDecryptAsync(bytes);
        }

        private byte[] TripleDesDecrypt(byte[] encrypted)
        {
            return TripleDesDecryptAsync(encrypted, true).GetAwaiter().GetResult();
        }

        private Task<byte[]> TripleDesDecryptAsync(byte[] encrypted)
        {
            return TripleDesDecryptAsync(encrypted, false);
        }

        private async Task<byte[]> TripleDesDecryptAsync(byte[] encrypted, bool sync)
        {
            Guard.AgainstNull(encrypted, nameof(encrypted));

            byte[] plainBytes;

            using (var ms = new MemoryStream(encrypted.Length))
            using (var cs = new CryptoStream(ms, _provider.CreateDecryptor(), CryptoStreamMode.Write))
            {
                if (sync)
                {
                    cs.Write(encrypted, 0, encrypted.Length);
                }
                else
                {
                    await cs.WriteAsync(encrypted, 0, encrypted.Length).ConfigureAwait(false);
                }

                cs.FlushFinalBlock();

                plainBytes = new byte[(int)ms.Length];

                ms.Position = 0;

                _ = sync
                    ? ms.Read(plainBytes, 0, (int)ms.Length)
                    : await ms.ReadAsync(plainBytes, 0, (int)ms.Length).ConfigureAwait(false);
            }

            return plainBytes;
        }

        private async Task<byte[]> TripleDesEncryptAsync(byte[] plain, bool sync)
        {
            Guard.AgainstNull(plain, nameof(plain));

            byte[] encryptedBytes;

            using (var ms = new MemoryStream(plain.Length * 2 - 1))
            using (var cs = new CryptoStream(ms, _provider.CreateEncryptor(), CryptoStreamMode.Write))
            {
                if (sync)
                {
                    cs.Write(plain, 0, plain.Length);
                }
                else
                {
                    await cs.WriteAsync(plain, 0, plain.Length).ConfigureAwait(false);
                }

                cs.FlushFinalBlock();

                encryptedBytes = new byte[(int)ms.Length];

                ms.Position = 0;

                _ = sync
                    ? ms.Read(encryptedBytes, 0, (int)ms.Length)
                    : await ms.ReadAsync(encryptedBytes, 0, (int)ms.Length).ConfigureAwait(false);
            }

            return encryptedBytes;
        }
    }
}