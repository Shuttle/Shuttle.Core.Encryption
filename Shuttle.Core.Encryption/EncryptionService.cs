﻿using System;
using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly Dictionary<string, IEncryptionAlgorithm> _encryptionAlgorithms = new Dictionary<string, IEncryptionAlgorithm>();

        public EncryptionService(IEnumerable<IEncryptionAlgorithm> encryptionAlgorithms = null)
        {
            foreach (var encryptionAlgorithm in encryptionAlgorithms ?? Enumerable.Empty<IEncryptionAlgorithm>())
            {
                Add(encryptionAlgorithm);
            }
        }

        public IEncryptionService Add(IEncryptionAlgorithm encryptionAlgorithm)
        {
            Guard.AgainstNull(encryptionAlgorithm, nameof(encryptionAlgorithm));

            if (_encryptionAlgorithms.ContainsKey(encryptionAlgorithm.Name))
            {
                throw new ArgumentException(string.Format(Resources.DuplicateEncryptionAlgorithmException,
                    encryptionAlgorithm.Name));
            }

            _encryptionAlgorithms.Add(encryptionAlgorithm.Name, encryptionAlgorithm);

            return this;
        }

        public IEncryptionAlgorithm Get(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            if (!_encryptionAlgorithms.ContainsKey(name))
            {
                throw new ArgumentException(string.Format(Resources.EncryptionAlgorithmMissingException, name));
            }

            return _encryptionAlgorithms[name];
        }

        public bool Contains(string name)
        {
            return _encryptionAlgorithms.ContainsKey(Guard.AgainstNullOrEmptyString(name, nameof(name)));
        }

        public IEnumerable<IEncryptionAlgorithm> Algorithms => _encryptionAlgorithms.Values;
    }
}