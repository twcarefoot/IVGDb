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

        /// <summary>
        /// Db context.
        /// </summary>
        public static ivgdb_Entities db = new ivgdb_Entities();

        /// <summary>
        /// Returns the VIDEO GAME by their gameID
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public static VideoGame GetGameByID(int gameID)
        {
            if (db.VideoGames.Any(p => p.GameID == gameID))
                return db.VideoGames.Where(p => p.GameID == gameID).FirstOrDefault();
            else
                return null;
        }

        /// <summary>
        /// Returns a list of consoles a game is released on.
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public static List<GamesFor> GetGameConsoles(int gameID)
        {
            if (db.GamesFors.Any(p => p.GameID == gameID))
                return db.GamesFors.Where(p => p.GameID == gameID).ToList();
            else
                return null;
        }

        /// <summary>
        /// Returns a game by searching their title.
        /// Used in the search bar method.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static VideoGame GetGameByTitle(string title)
        {
            if (db.VideoGames.Any(p => p.Title == title))
                return db.VideoGames.Where(p => p.Title == title).First();
            else
                return null;
        }
        
        /// <summary>
        /// Returns all games in the video game database.
        /// </summary>
        /// <returns></returns>
        public static List<VideoGame> GetAllGames()
        {
            return db.VideoGames.ToList();
        }

        /// <summary>
        /// Queries the database based of of the title.
        /// Orders in alphabetical order.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static List<VideoGame> QueryGames(string title)
        {
            return db.VideoGames.Where(p => p.Title.Contains(title)).OrderBy(p => p.Title).ToList();
        }

        /// <summary>
        /// Homepage TV displays the newest additions to the video game database.
        /// </summary>
        /// <returns></returns>
        public static List<VideoGame> GetFiveNewest()
        {
            return db.VideoGames.OrderByDescending(p => p.GameID).ToList();
        }
    }
}
