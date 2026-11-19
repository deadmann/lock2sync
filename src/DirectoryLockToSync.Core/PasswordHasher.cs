using System;
using System.Security.Cryptography;

namespace DirectoryLockToSync;

/// <summary>
/// Stores passwords as a salted PBKDF2 hash. The 2008 version kept the password as plain text in its settings.
/// Stored form: <c>pbkdf2-sha256$iterations$salt$hash</c> (salt and hash in Base64).
/// </summary>
public static class PasswordHasher
{
    private const string Prefix = "pbkdf2-sha256";
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    public static string Hash(string password)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));

        var salt = new byte[SaltSize];
        using (var random = RandomNumberGenerator.Create())
        {
            random.GetBytes(salt);
        }

        var hash = Derive(password, salt, Iterations, HashSize);
        return Prefix + "$" + Iterations + "$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(hash);
    }

    /// <summary>Checks a password against a stored value. Anything empty or malformed simply does not match.</summary>
    public static bool Verify(string password, string stored)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));
        if (string.IsNullOrEmpty(stored)) return false;

        var parts = stored.Split('$');
        if (parts.Length != 4 || parts[0] != Prefix) return false;
        if (!int.TryParse(parts[1], out var iterations) || iterations < 1) return false;

        byte[] salt;
        byte[] expected;
        try
        {
            salt = Convert.FromBase64String(parts[2]);
            expected = Convert.FromBase64String(parts[3]);
        }
        catch (FormatException)
        {
            return false;
        }

        if (expected.Length == 0) return false;

        var actual = Derive(password, salt, iterations, expected.Length);

        // Compare every byte, so the time taken does not reveal how much of the password was right.
        var difference = 0;
        for (var i = 0; i < expected.Length; i++) difference |= expected[i] ^ actual[i];
        return difference == 0;
    }

    // PBKDF2 with HMAC-SHA256. Both branches give the same bytes; .NET 10 deprecated the constructor used for .NET Framework.
    private static byte[] Derive(string password, byte[] salt, int iterations, int length)
    {
#if NET
        return Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, length);
#else
        using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
        {
            return kdf.GetBytes(length);
        }
#endif
    }
}
