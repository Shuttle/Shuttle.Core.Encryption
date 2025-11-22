using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption;

public class EncryptionBuilder(IServiceCollection services)
{
    public IServiceCollection Services { get; } = Guard.AgainstNull(services);

    public EncryptionBuilder AddNull()
    {
        Services.AddSingleton<IEncryptionAlgorithm, NullEncryptionAlgorithm>();

        return this;
    }
}