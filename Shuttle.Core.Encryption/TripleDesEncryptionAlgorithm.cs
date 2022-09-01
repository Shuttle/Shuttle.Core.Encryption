using System;
using System.IO;
using System.Security.Cryptography;
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

        public byte[] Decrypt(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            return TripleDesDecrypt(bytes);
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

                encryptedBytes = new byte[(int) ms.Length];

                ms.Position = 0;

                _ = ms.Read(encryptedBytes, 0, (int) ms.Length);
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

                plainBytes = new byte[(int) ms.Length];

                ms.Position = 0;

                _ = ms.Read(plainBytes, 0, (int) ms.Length);
            }

            return plainBytes;
        }
    }
}