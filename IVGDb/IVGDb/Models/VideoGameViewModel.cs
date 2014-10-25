using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVGDb;

namespace IVGDb.Models
{
    public class VideoGameViewModel : VideoGame
    {
        public static ivgdb_Entities db = new ivgdb_Entities();

        public static VideoGame GetGameByID(int gameID)
        {
            return db.VideoGames.Where(p => p.GameID == gameID).First();
        }
        public static VideoGame GetGameByTitle(string title)
        {
            return db.VideoGames.Where(p => p.Title == title).First();
        }
    }
}