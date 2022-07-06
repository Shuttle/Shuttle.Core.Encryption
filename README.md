# Shuttle.Core.Encryption

```
PM> Install-Package Shuttle.Core.Encryption
```

Provides an encryption adapter through the `IEncryptionAlgorithm` interface.

Implementations available in this package:

- `TripleDesEncryptionAlgorithm`
- `NullEncryptionAlgorithm`

There is also an `IEncryptionService` that acts as a central container for all registered `IEncryptionAlgorithm` implementations.

## Configuration

In order to add encryption:

```
services.AddEncryption(options => {
	options.AddTripleDes(key);
});
```

Will try to add the `EncryptionService` singleton. with an option to add the `TripleDesEncryptionAlgorithm` instance using the given symmetric `key`.  The key may also be read from configuration by not specifying it, in which case the following default structure will be used:

```
{
	"Shuttle": {
		"TripleDes": {
			"Key": "triple-des-key"
		}
	}
}
```