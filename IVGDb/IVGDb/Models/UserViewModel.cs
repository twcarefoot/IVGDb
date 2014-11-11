using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVGDb.Models
{
    public class UserViewModel : User
    {
        public string ConfirmPassword { get; set; }

        private static ivgdb_Entities db = new ivgdb_Entities();

        public static User GetUser(string email)
        {
            return db.Users.Where(p => p.Email == email).First();
        }

        public static string GetCurrentPassword(string username)
        {
            return db.Users.Where(p => p.Username == username).Select(p => p.Password).First();
        }

        public static string HashPassword(string password)
        {
            return PasswordHash.PasswordHash.CreateHash(password);
        }

        public static bool VerifyPassword(string username, string enteredPassword)
        {
            string storedHashPassword = GetCurrentPassword(username);
            return PasswordHash.PasswordHash.ValidatePassword(enteredPassword, storedHashPassword);
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
        }
    }
}