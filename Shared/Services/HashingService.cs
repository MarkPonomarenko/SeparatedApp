using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    internal class HashingService
    {
        public static string GetPasswordHash(string password)
        {
            var MD5Inst = MD5.Create();
            var passwordHash = MD5Inst.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(passwordHash);
        }
    }
}
