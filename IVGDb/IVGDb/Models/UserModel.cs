using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVGDb.Models
{
    public class UserModel : User
    {
        public string ConfirmPassword { get; set; }

        //private static ivgdb_Entities db = new ivgdb_Entities();

        public static User GetUser(string username)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Users.Where(p => p.Username == username).First();
            }
        }

        public static string GetCurrentPassword(string username)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Users.Where(p => p.Username == username).Select(p => p.Password).ToString();
            }
        }

        public static bool MatchingPasswords(string username, string newPassword)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Users.Where(p => p.Username == username).Select(p => p.Password).Equals(newPassword);
            }
        }

        public static void Register(UserModel user)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                User newUser = new User();
                newUser.Username = user.Username;
                newUser.Email = user.Email;
                newUser.Password = user.Password;
                newUser.UserProfilePicLink = null;
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
    }
}