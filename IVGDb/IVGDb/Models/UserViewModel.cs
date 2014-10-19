using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVGDb.Models
{
    public class UserViewModel : User
    {
        public string ConfirmPassword { get; set; }

        private static ivgdbEntities db = new ivgdbEntities();

        public static User GetUser(string email)
        {
            return db.Users.Where(p => p.Email == email).First();
        }

        public static string GetCurrentPassword(string email)
        {
            return db.Users.Where(p => p.Email == email).Select(p => p.Password).ToString();
        }

        public static bool MatchingPasswords(string email, string newPassword)
        {
            return db.Users.Where(p => p.Email == email).Select(p => p.Password).Equals(newPassword);
        }

        public static void Register(UserViewModel user)
        {
            User newUser = new User();
            newUser.Username = user.Username;
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            newUser.UserProfilePicLink = null;
            db.Users.Add(newUser);
            db.SaveChanges();
            db.Dispose();
        }
    }
}