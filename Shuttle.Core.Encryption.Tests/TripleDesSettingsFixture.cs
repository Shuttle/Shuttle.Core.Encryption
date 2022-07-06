using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Shuttle.Core.Encryption.Tests
{
    [TestFixture]
    public class TripleDesSettingsFixture
    {
        private IConfigurationSection GetSection(string file)
        {
            return new ConfigurationBuilder().AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@".\{file}")).Build().GetSection(TripleDesSettings.SectionName);
        }

        [Test]
        [TestCase("appsettings.json")]
        public void Should_be_able_to_load_the_TripleDes_section(string file)
        {
            var settings = new TripleDesSettings();

            GetSection(file).Bind(settings);

            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.Key);
            Assert.AreEqual("triple-des-key", settings.Key);
        }
    }
}