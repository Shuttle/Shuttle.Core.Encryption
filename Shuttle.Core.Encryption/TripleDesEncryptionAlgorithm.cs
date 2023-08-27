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

            return TripleDesEncrypt(bytes);
        }

        public async Task<byte[]> EncryptAsync(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return await TripleDesEncryptAsync(bytes);
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

        private byte[] TripleDesEncrypt(byte[] plain)
        {
            return GetEncryptedBytes(plain.Length, plain);
        }

        private byte[] GetEncryptedBytes(int plainLength, byte[] plainBytes)
        {
            byte[] encryptedBytes;

            using (var ms = new MemoryStream(plainLength * 2 - 1))
            using (var cs = new CryptoStream(ms, _provider.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);

                cs.FlushFinalBlock();

                encryptedBytes = new byte[(int)ms.Length];

                ms.Position = 0;

                _ = ms.Read(encryptedBytes, 0, (int)ms.Length);
            }

            return encryptedBytes;
        }

        private async Task<byte[]> TripleDesEncryptAsync(byte[] plain)
        {
            return await GetEncryptedBytesAsync(plain.Length, plain);
        }

        private async Task<byte[]> GetEncryptedBytesAsync(int plainLength, byte[] plainBytes)
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

        private byte[] TripleDesDecrypt(byte[] encrypted)
        {
            return GetPlainBytes(encrypted.Length, encrypted);
        }

        private byte[] GetPlainBytes(int secureLength, byte[] encryptedBytes)
        {
            byte[] plainBytes;

            using (var ms = new MemoryStream(secureLength))
            using (var cs = new CryptoStream(ms, _provider.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(encryptedBytes, 0, encryptedBytes.Length);

                cs.FlushFinalBlock();

                plainBytes = new byte[(int)ms.Length];

                ms.Position = 0;

                _ = ms.Read(plainBytes, 0, (int)ms.Length);
            }

            return plainBytes;
        }

        private Task<byte[]> TripleDesDecryptAsync(byte[] encrypted)
        {
            return GetPlainBytesAsync(encrypted.Length, encrypted);
        }

        private async Task<byte[]> GetPlainBytesAsync(int secureLength, byte[] encryptedBytes)
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