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
