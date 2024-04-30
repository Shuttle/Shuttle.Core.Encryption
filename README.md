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

```c#
services.AddEncryption(builder => {
	builder.AddTripleDes(tripleDesOptions);
});
```

Will try to add the `EncryptionService` singleton, with an option to add the `TripleDesEncryptionAlgorithm` instance using the given symmetric `Key`.

The default JSON settings structure is as follows:

```json
{
	"Shuttle": {
		"TripleDes": {
			"Key": "triple-des-key"
		}
	}
}
```

## Usage

```c#
var algorithm = encryptionService.Get("algorithm-name");
var encrypted = await algorithm.EncryptAsync(Encoding.UTF8.GetBytes("some data"));
var decrypted = await algorithm.DecryptAsync(encrypted);
```