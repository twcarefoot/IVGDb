using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVGDb.Models
{
    public class UserViewModel : User
    {
        public List<UsersRole> AdminList { get; set; }

        public string ConfirmPassword { get; set; }

        public int Role { get; set; }

        public static UserViewModel CreateUserViewModel(User user)
        {
            UserViewModel uvm = new UserViewModel();

            uvm.UserID = user.UserID;
            uvm.Username = user.Username;
            uvm.Password = user.Password;
            uvm.Email = user.Email;
            uvm.UserProfilePicLink = user.UserProfilePicLink;            

            return uvm;
        }

        public List<VideoGame> TopFiveLatest { get; set; }

        private static ivgdb_Entities db = new ivgdb_Entities();

        public static List<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public static List<UsersRole> GetAllUsersRoles()
        {
            return db.UsersRoles.ToList();
        }

        public static User GetUser(string username)
        {
            return db.Users.Where(p => p.Username == username).First();
        }

        public static string GetCurrentPassword(string username)
        {
            return db.Users.Where(p => p.Username == username).Select(p => p.Password).First();
        }

        public static int GetUserRole(string username)
        {
            int userID = db.Users.Where(p => p.Username == username).Select(p => p.UserID).First();
            return db.UsersRoles.Where(p => p.UserID == userID).Select(p => p.RoleID).First();
        }

        public static string GetUserImageURL(string username)
        {
            return db.Users.Where(p => p.Username == username).Select(p => p.UserProfilePicLink).First();
        }

        public static string HashPassword(string password)
        {
            return PasswordHash.PasswordHash.CreateHash(password);
        }

        public static bool IsUserBanned(string username)
        {
            int userID = db.Users.Where(p => p.Username == username).Select(p => p.UserID).First();
            int role = db.UsersRoles.Where(p => p.UserID == userID).Select(p => p.RoleID).First();

            if (role == 3)
                return true;

            return false;
        }

        public static bool UserExists(string username)
        {
            return db.Users.Any(p => p.Username == username);
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

            UsersRole role = new UsersRole();
            User addedUser = GetUser(user.Username);
            role.UserID = addedUser.UserID;
            role.RoleID = 2;
            db.UsersRoles.Add(role);
            db.SaveChanges();
        }

        public static void SetProfilePicURL(User user)
        {
            User updatingUser = GetUser(user.Username);
            updatingUser.UserProfilePicLink = user.UserProfilePicLink;
            db.SaveChanges();
        }
    }
}