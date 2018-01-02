using System;
using System.IO;
using NUnit.Framework;
using Shuttle.Core.Configuration;

namespace Shuttle.Core.Encryption.Tests
{
    [TestFixture]
    public class TripleDesSectionFixture
    {
        private TripleDesSection GetSection(string file)
        {
            return ConfigurationSectionProvider.OpenFile<TripleDesSection>("shuttle", "tripleDes",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@".\files\{file}"));
        }

        [Test]
        [TestCase("TripleDes.config")]
        [TestCase("TripleDes-Grouped.config")]
        public void Should_be_able_to_load_the_TripleDes_section(string file)
        {
            var section = GetSection(file);

            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Key);
            Assert.AreEqual("triple-des-key", section.Key);
        }
    }
}