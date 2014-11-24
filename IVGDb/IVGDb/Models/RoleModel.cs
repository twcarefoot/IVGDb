using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVGDb.Models
{
    public class RoleModel : Role
    {
        public static List<Role> GetRoles()
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Roles.ToList();
            }
        }

        public static UsersRole GetUsersRole(string username)
        {
            using(ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.UsersRoles.Where(p => p.User.Username == username).First();
            }
        }

        public static void Update(string username, int role)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                UsersRole updatingRole = GetUsersRole(username);
                updatingRole.RoleID = role;
                db.SaveChanges();
            }
        }
    }
}