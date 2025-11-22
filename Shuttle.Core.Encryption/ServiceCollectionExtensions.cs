using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEncryption(Action<EncryptionBuilder>? builder = null)
        {
            Guard.AgainstNull(services);

            var encryptionBuilder = new EncryptionBuilder(services);

            builder?.Invoke(encryptionBuilder);

            services.TryAddSingleton<IEncryptionService, EncryptionService>();

            return services;
        }
    }
}