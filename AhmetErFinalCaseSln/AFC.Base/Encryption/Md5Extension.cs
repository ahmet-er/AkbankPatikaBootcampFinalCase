using System.Security.Cryptography;
using System.Text;

namespace AFC.Base.Encryption;

public class Md5Extension
{
    public static string Create(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
    public static string GetHash(string input)
    {
        var hash = Create(input);
        return Create(hash);
    }
}
