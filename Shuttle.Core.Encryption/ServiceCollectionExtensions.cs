using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEncryption(this IServiceCollection services, Action<EncryptionBuilder> builder = null)
        {
            Guard.AgainstNull(services, nameof(services));

            var encryptionBuilder = new EncryptionBuilder(services);

            builder?.Invoke(encryptionBuilder);

            services.TryAddSingleton<IEncryptionService, EncryptionService>();

            return services;
        }
    }
}