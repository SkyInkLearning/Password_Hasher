## Password hashing CLI (From chatgpt)

### Purpose:

The reason for this is that I am making a site to which only I am going to ever log into, for now. So im only going to have a single admin account on the site. When learning more about a possible solution, I was told that I might want to hash the password that I was going to store on azure so to learn more I had chatgpt create a program with which I can get hashed versions of a word, together with a salt, to use for azure.

### Packages:

Install-Package Microsoft.AspNetCore.Cryptography.KeyDerivation

### The code:

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

### How to run:

Go to folder where program file is in.

Open dev console and type in:

dotnet run -- *password you want to hash*

It will take a moment and then give you the hashed password and the salt key used.

<img src="https://github.com/user-attachments/assets/bc4c6d79-d4ac-4dfc-a70f-2f56c191e28b" height="400">

