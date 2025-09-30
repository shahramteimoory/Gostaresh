using System.Security.Cryptography;
using System.Text;

namespace MachineReporting.Api.Utilities
{
    public static class SecurityHelper
    {
        public static string HashSha256(this string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = sha256.ComputeHash(byteValue);
                return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
            }
        }

        public static bool Verify(string value, string hashed)
        {
            string hashedInput = HashSha256(value);
            return string.Equals(hashedInput, hashed);
        }
    }
}