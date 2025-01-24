using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption;

public static class TripleDesEncryptionExtensions
{
    public static EncryptionBuilder AddTripleDes(this EncryptionBuilder builder, TripleDesOptions tripleDesOptions)
    {
        Guard.AgainstNull(builder).Services
            .AddSingleton<IEncryptionAlgorithm, TripleDesEncryptionAlgorithm>()
            .AddSingleton<IValidateOptions<TripleDesOptions>, TripleDesOptionsValidator>();

        builder.Services.Configure<TripleDesOptions>(options =>
        {
            options.Key = tripleDesOptions.Key;
        });

        return builder;
    }
}