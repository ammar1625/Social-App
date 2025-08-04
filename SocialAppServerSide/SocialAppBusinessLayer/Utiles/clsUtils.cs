using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace SocialAppBusinessLayer.Utiles
{
    public static class clsUtils
    {
        static public string HashPassWord(string PassWord)
        {
            return BCrypt.Net.BCrypt.HashPassword(PassWord,BCrypt.Net.BCrypt.GenerateSalt());
        }

        public static bool VerifyPassWord(string PassWord, string PasswordHash) 
        {
            return BCrypt.Net.BCrypt.Verify(PassWord,PasswordHash);
        }
    }
}
