using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVGDb;

namespace IVGDb.Models
{
    public class VideoGameViewModel : VideoGame
    {
        public UserViewModel user { get; set; }

        public static ivgdb_Entities db = new ivgdb_Entities();

        public static VideoGame GetGameByID(int gameID)
        {
            if (db.VideoGames.Any(p => p.GameID == gameID))
                return db.VideoGames.Where(p => p.GameID == gameID).FirstOrDefault();
            else
                return null;
        }

        public static List<GamesFor> GetGameConsoles(int gameID)
        {
            return db.GamesFors.Where(p => p.GameID == gameID).ToList();
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

        public static List<VideoGame> QueryGames(string title)
        {
            return db.VideoGames.Where(p => p.Title.Contains(title)).ToList();
        }
    }
}
