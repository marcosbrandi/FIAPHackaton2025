using System.Security.Cryptography;

namespace HM.API.Services;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        // Gerar um salt aleatório
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        // Derivar uma chave de 256 bits usando PBKDF2 com 10000 iterações
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        // Combinar o salt e o hash
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        // Convertê-los para string Base64
        string savedPasswordHash = Convert.ToBase64String(hashBytes);
        return savedPasswordHash;
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // Extrair o byte array do hash armazenado
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // Pegar o salt do hash armazenado
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        // Derivar a chave do password digitado
        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        // Comparar byte por byte
        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
                return false;
        }
        return true;
    }
}
