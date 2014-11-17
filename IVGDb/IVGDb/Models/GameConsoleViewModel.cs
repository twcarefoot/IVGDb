using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVGDb.Models;

namespace IVGDb.Models
{
    public class GameConsoleViewModel : VideoGame
    {
        public List<string> chosenConsoles { get; set; } //Attribute for chosen game consoles for new game entry

        public static List<Console> getAllConsoles(){
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Consoles.ToList();
            }
        }
    }
}