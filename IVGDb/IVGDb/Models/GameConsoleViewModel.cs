using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVGDb.Models
{
    public class GameConsoleViewModel : VideoGame
    {
        public static List<Console> ChosenConsoles { get; set; }

        public static List<Console> GetAllConsoles()
        {
            using(ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Consoles.ToList();
            }
        }
    }
}