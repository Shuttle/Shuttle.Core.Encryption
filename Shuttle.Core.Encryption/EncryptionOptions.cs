using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public class EncryptionOptions
    {
        private readonly IServiceCollection _services;

        public EncryptionOptions(IServiceCollection services)
        {
            Guard.AgainstNull(services, nameof(services));

            _services = services;
        }

        public EncryptionOptions AddTripleDes(string key)
        {
            _services.AddOptions<TripleDesSettings>().Configure(options =>
            {
                options.Key = key;
            });

            return this;
        }

        public EncryptionOptions AddTripleDes()
        {
            _services.AddOptions<TripleDesSettings>().Configure<IConfiguration>((options, configuration) =>
            {
                options.Key = configuration.GetSection(TripleDesSettings.SectionName)?.Key;
            });

            return this;
        }
    }
}