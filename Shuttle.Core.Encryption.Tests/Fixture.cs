using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Shuttle.Core.Encryption.Tests;

[TestFixture]
public class Fixture
{
    [Test]
    public async Task Should_be_able_to_encrypt_and_decrypt_using_3des_async()
    {
        var algorithm = new TripleDesEncryptionAlgorithm(new OptionsWrapper<TripleDesOptions>(new() { Key = Guid.NewGuid().ToString() }));

        const string text = "triple des encryption algorithm";

        Assert.That(Encoding.UTF8.GetString(await algorithm.DecryptAsync(await algorithm.EncryptAsync(Encoding.UTF8.GetBytes(text)))), Is.EqualTo(text));
    }
}