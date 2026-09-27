using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    // Formato: {iterations}.{saltBase64}.{hashBase64}
    public static string HashPassword(string password, int iterations = 10000)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
            }
        }
    }

    public static bool Verify(string hashedPassword, string password)
    {
        if (string.IsNullOrEmpty(hashedPassword)) return false;

        var parts = hashedPassword.Split('.');
        if (parts.Length != 3) return false;

        if (!int.TryParse(parts[0], out int iterations)) return false;

        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] hash = Convert.FromBase64String(parts[2]);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
        {
            byte[] computed = pbkdf2.GetBytes(hash.Length);
            return AreEqual(computed, hash);
        }
    }

    private static bool AreEqual(byte[] a, byte[] b)
    {
        if (a.Length != b.Length) return false;
        int diff = 0;
        for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
        return diff == 0;
    }
}
