[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Encoding?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Encoding/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Encoding.svg)](https://www.nuget.org/packages/ByteDev.Encoding)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Encoding/blob/master/LICENSE)

# ByteDev.Encoding

Library of encoding/decoding related functionality for Base64 and Hexadecimal.

## Installation

ByteDev.Encoding has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Encoding is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Encoding`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Encoding/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Encoding/blob/master/docs/RELEASE-NOTES.md).

## Code

The repo can be cloned from git bash:

`git clone https://github.com/ByteDev/ByteDev.Encoding`

## Usage

The main library classes include:
- Base64Encoder
- HexEncoder
- Serializer
- EncoderFactory

### Base64Encoder

`Base64Encoder` provides a way to encode to hexadecimal strings and decode back again.

```csharp
IEncoder encoder = new Base64Encoder();

string base64 = encoder.Encode("John");

// base64 == "Sm9obg=="

string text = encoder.Decode(base64);

// text == "John"
```

### HexEncoder

`HexEncoder` provides a way to encode to hexadecimal strings and decode back again.

```csharp
IEncoder encoder = new HexEncoder('='); // optional delimiter arg

string hex = encoder.Encode("John");

// hex == "4A=6F=68=6E"

string text = encoder.Decode(hex);

// text == "John"
```

### Serializer

The `Serializer` class provides a way to serialize/deserialize objects based on the provided `IEncoder` implementation (Base64 or Hex).

```csharp
// Entity to serialize
[Serializable]
public class Person
{
    public string Name { get; set; }
}

// ...

var person = new Person { Name = "John Smith" }
```

```csharp
// Setup serializer
IEncoder encoder = new Base64Encoder();

ISerializer serializer = new Serializer(encoder);
```

```csharp
// Serialize
string base64 = serializer.Serialize(person);
```

```csharp
// Deserialize
Person person = serializer.Deserialize<Person>(base64);
```

### EncoderFactory

The `EncoderFactory` provides a convenient to create a type of encoder based on the `EncodingType`.

```csharp
IEncoderFactory factory = new EncoderFactory();

IEncoder encoder = factory.Create(EncodingType.Base64);
```