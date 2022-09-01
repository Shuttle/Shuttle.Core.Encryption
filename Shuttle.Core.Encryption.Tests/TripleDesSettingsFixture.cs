using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Shuttle.Core.Encryption.Tests
{
    [TestFixture]
    public class TripleDesSettingsFixture
    {
        private IConfigurationSection GetSettings()
        {
            return new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@".\appsettings.json")).Build()
                .GetSection(TripleDesOptions.SectionName);
        }

        [Test]
        public void Should_be_able_to_load_the_TripleDes_section()
        {
            var settings = new TripleDesOptions();

            GetSettings().Bind(settings);

            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.Key);
            Assert.AreEqual("triple-des-key", settings.Key);
        }
    }
}