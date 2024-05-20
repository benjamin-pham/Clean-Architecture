using Application.Libraries;
using System.Security.Cryptography;
using System.Text;

namespace Shared;
public class Hash : IHash
{
    public string HashSHA1(string value)
    {
        var sha1 = SHA1.Create();
        var inputBytes = Encoding.ASCII.GetBytes(value);
        var hash = sha1.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }
}
