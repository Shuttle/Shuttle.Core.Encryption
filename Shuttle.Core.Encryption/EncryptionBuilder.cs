using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public class EncryptionBuilder
    {
        public EncryptionBuilder(IServiceCollection services)
        {
            Services = Guard.AgainstNull(services, nameof(services));
        }

        public IServiceCollection Services { get; }
        public TripleDesOptions TripleDesOptions { get; set; } = new TripleDesOptions();

        public EncryptionBuilder AddTripleDes()
        {
            Services.AddSingleton<IEncryptionAlgorithm, TripleDesEncryptionAlgorithm>();

            return this;
        }

        public EncryptionBuilder AddNull()
        {
            Services.AddSingleton<IEncryptionAlgorithm, NullEncryptionAlgorithm>();

            return this;
        }
    }
}