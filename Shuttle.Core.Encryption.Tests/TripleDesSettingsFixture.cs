using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Shuttle.Core.Encryption.Tests;

[TestFixture]
public class TripleDesSettingsFixture
{
    private IConfigurationSection GetSettings()
    {
        return new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @".\appsettings.json")).Build()
            .GetSection(TripleDesOptions.SectionName);
    }

    [Test]
    public void Should_be_able_to_load_the_TripleDes_section()
    {
        var options = new TripleDesOptions();

        GetSettings().Bind(options);

        Assert.That(options, Is.Not.Null);
        Assert.That(options.Key, Is.Not.Null);
        Assert.That(options.Key, Is.EqualTo("triple-des-key"));
    }
}