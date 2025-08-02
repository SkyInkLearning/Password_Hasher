# Password hashing CLI (From chatgpt)

## Purpose:

I let chatgpt create a program with which I can get hashed versions of a username and password, together with a salt, to use for azure. 

The reason for this is that I am making a sitez to which only I am going to ever log into. So im only going to have a single admin account on the site. Im going to add the admin credentials to azure when I deploy and wanted to have the password hashed. 

## How to run:

Go to folder where program file is in.

Open dev console and type in:

dotnet run -- *password you want to hash*

It will take a moment and then give you the hashed password and the salt key used.

## Packages:

Install-Package Microsoft.AspNetCore.Cryptography.KeyDerivation

## The code:

```csharp
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run <password>");
    return;
}

string password = args[0];

// Generate a 16-byte salt
byte[] salt = new byte[16];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(salt);
}

// Hash the password
string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
    password: password,
    salt: salt,
    prf: KeyDerivationPrf.HMACSHA256,
    iterationCount: 10000,
    numBytesRequested: 32
));

Console.WriteLine("PasswordHash: " + hashed);
Console.WriteLine("Salt: " + Convert.ToBase64String(salt));
```
