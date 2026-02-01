# Shuttle.Core.Encryption

Provides an encryption adapter through the `IEncryptionAlgorithm` interface.

Implementations available in this package:

- `TripleDesEncryptionAlgorithm` (name '3DES')
- `NullEncryptionAlgorithm` (name 'nukk')

There is also an `IEncryptionService` that acts as a central container for all registered `IEncryptionAlgorithm` implementations.

## Installation

```bash
dotnet add package Shuttle.Core.Encryption
```

## Configuration

```c#
services.AddEncryption(builder => {
    builder.AddTripleDes(options => {
        options.Key = "your-secret-key-here";
    });
});
```

### JSON Configuration

The default JSON settings structure reads from your app configuration:

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

### Basic Usage

```c#
// Get the encryption service via dependency injection
var encryptionService = serviceProvider.GetRequiredService<IEncryptionService>();

// Get a specific encryption algorithm by name
var algorithm = encryptionService.Get("3DES");

// Encrypt data
var plainData = Encoding.UTF8.GetBytes("some data");
var encrypted = await algorithm.EncryptAsync(plainData);

// Decrypt data
var decrypted = await algorithm.DecryptAsync(encrypted);
var decryptedText = Encoding.UTF8.GetString(decrypted);
```