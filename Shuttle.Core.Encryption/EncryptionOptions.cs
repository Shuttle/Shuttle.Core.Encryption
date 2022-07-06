using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public class EncryptionOptions
    {
        public EncryptionOptions(IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));

            Services = services;
        }

        public IServiceCollection Services { get; }

        public EncryptionOptions AddTripleDes(string key)
        {
            Services.AddOptions<TripleDesSettings>().Configure(options =>
            {
                options.Key = key;
            });

            return this;
        }

        public EncryptionOptions AddTripleDes()
        {
            Services.AddOptions<TripleDesSettings>().Configure<IConfiguration>((options, configuration) =>
            {
                options.Key = configuration.GetSection(TripleDesSettings.SectionName)?.Key;
            });

            return this;
        }
    }
}