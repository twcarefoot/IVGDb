using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVGDb;

namespace IVGDb.Models
{
    public class GameViewModel : VideoGame
    {
        public static ivgdb_dbEntities db = new ivgdb_dbEntities();

        public static GameViewModel GetGame(string title)
        {
            return db.VideoGames.Where(p => p.Title == title).First();
        }
    }
}