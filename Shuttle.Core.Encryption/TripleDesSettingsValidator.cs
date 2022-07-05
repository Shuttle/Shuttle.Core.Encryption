using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Encryption
{
    public class TripleDesSettingsValidator : IValidateOptions<TripleDesSettings>
    {
        public ValidateOptionsResult Validate(string name, TripleDesSettings options)
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