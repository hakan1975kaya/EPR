using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public static class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VeryfyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }

        }
        public static string CreatePasswordHashSha256(string password)
        {
            var sb = new StringBuilder();
            var bytes = Encoding.UTF8.GetBytes(password);
            var algo = HashAlgorithm.Create(nameof(SHA256));
            var hash = algo!.ComputeHash(bytes);
            if (hash != null)
            {
                if (hash.Length > 0)
                {
                    foreach (var byt in hash)
                    {
                        sb.Append(byt.ToString("x2"));
                    }
                }
            }
            return sb.ToString();
        }
        public static string CreateRandomToken(string password)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            int length = 50;

            var random = new Random();

            var randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }
        public static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }















    }
}
