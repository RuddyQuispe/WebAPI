using Domain.Entities.UserModule;
using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Extensions;

public static class UserExtensions
{
    private const int keySize = 64;
    private const int iterations = 350000;
    private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public static void HashPasword(this User user)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(user.password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);
        user.PasswordSalt = Convert.ToHexString(salt);
        user.PasswordHash = Convert.ToHexString(hash);
    }

    public static bool VerifyPassword(this User user, string password)
    {
        byte[] PasswordSalt = Convert.FromHexString(user.PasswordSalt);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, PasswordSalt, iterations, hashAlgorithm, keySize);
        
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(user.PasswordHash));
    }
}
