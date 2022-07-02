using System.Configuration;
using Shuttle.Core.Configuration;

namespace Shuttle.Core.Encryption
{
    public class TripleDesSection : ConfigurationSection
    {
        [ConfigurationProperty("key", IsRequired = false, DefaultValue = null)]
        public string Key => (string) this["key"];

        public static ITripleDesConfiguration Configuration()
        {
            var section = ConfigurationSectionProvider.Open<TripleDesSection>("shuttle", "tripleDes");

            if (section == null)
            {
                throw new ConfigurationErrorsException(Resources.TripleDesSectionMissing);
            }

            if (string.IsNullOrEmpty(section.Key))
            {
                throw new ConfigurationErrorsException(Resources.TripleDesKeyMissing);
            }

            return new TripleDesConfiguration
            {
                Key = section.Key
            };
        }
    }
}