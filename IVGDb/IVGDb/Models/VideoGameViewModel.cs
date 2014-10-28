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
            if (db.VideoGames.Any(p => p.Title == title))
                return db.VideoGames.Where(p => p.Title == title).First();
            else
                return null;
        }

        public static List<VideoGame> GetAllGames()
        {
            return db.VideoGames.ToList();
        }
    }

}
