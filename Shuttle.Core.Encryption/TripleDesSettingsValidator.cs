using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public class TripleDesOptionsValidator : IValidateOptions<TripleDesOptions>
    {
        public ValidateOptionsResult Validate(string name, TripleDesOptions options)
        {
            Guard.AgainstNull(options, nameof(options));

            if (string.IsNullOrWhiteSpace(options.Key))
            {
                return ValidateOptionsResult.Fail(Resources.TripleDesKeyMissing);
            }

            return ValidateOptionsResult.Success;
        }
    }
}