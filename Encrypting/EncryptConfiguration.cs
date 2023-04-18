using System.Security.Cryptography;
using System.Text;

namespace GraduationThesis_CarServices.Encrypting
{
    public class EncryptConfiguration
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public string Base64Encode(string encodeStr)
        {
            var textbytes = Encoding.UTF8.GetBytes(encodeStr);
            return Convert.ToBase64String(textbytes);
        }

        public string Base64Decode(string decodeStr)
        {
            var strBytes = Convert.FromBase64String(decodeStr);
            return Encoding.UTF8.GetString(strBytes);
        }
    }
}