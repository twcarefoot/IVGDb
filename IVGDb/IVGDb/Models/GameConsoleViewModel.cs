using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVGDb.Models;

namespace IVGDb.Models
{
    public class GameConsoleViewModel : VideoGame
    {
        /// <summary>
        /// List of consoles chosen by the form.
        /// </summary>
        public List<bool> chosenConsoles { get; set; } //Attribute for chosen game consoles for new game entry

        /// <summary>
        /// List of video game consoles.
        /// </summary>
        public List<Console> consolesList { get; set; } //Attribute for chosen game consoles for new game entry

        /// <summary>
        /// Db context.
        /// </summary>
        private static ivgdb_Entities db = new ivgdb_Entities();

        /// <summary>
        /// Returns all consoles.
        /// </summary>
        /// <returns></returns>
        public static List<Console> getAllConsoles()
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Consoles.ToList();
            }
        }

        /// <summary>
        /// Gets all consoles to a list.
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
        /// Returns a game based off of the Game ID.
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
        /// Adds a new game to the database.
        ///Return the ID of the newly added game
        /// </summary>
        /// <param name="newGameModel"></param>
        /// <returns></returns>
        public static int AddNewGame(GameConsoleViewModel newGameModel)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                VideoGame newGame = new VideoGame();
                newGame.Title = newGameModel.Title;
                newGame.Publisher = newGameModel.Publisher;
                newGame.Developer = newGameModel.Developer;
                newGame.ReleaseDate = newGameModel.ReleaseDate;
                newGame.BoxArtLink = newGameModel.BoxArtLink;
                newGame.Synopsis = newGameModel.Synopsis;
                //Validation on title.
                if (newGame.Title != null)
                {
                    db.VideoGames.Add(newGame);
                    
                    for(int i = 0; i < newGameModel.chosenConsoles.Count; i++){
                        if (newGameModel.chosenConsoles[i] == true)
                        {
                            GamesFor newGamesFor = new GamesFor();
                            newGamesFor.ConsoleID = i+1;
                            newGamesFor.GameID = newGame.GameID;
                            db.GamesFors.Add(newGamesFor);
                        }
                    }
                    db.SaveChanges();
                    return newGame.GameID;
                }
                return 0;
            }
        }

        /// <summary>
        /// Delete a game from the database.
        /// We want users to be absolutely sure they want to delete.
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public static int DeleteGame(int gameID)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                List<GamesFor> gamesForList = db.GamesFors.Where(p => p.GameID == gameID).ToList();
                foreach (GamesFor gamesFor in gamesForList)
                {
                    db.GamesFors.Remove(gamesFor);
                }
                VideoGame gameDeleting = db.VideoGames.First(p => p.GameID == gameID);
                db.VideoGames.Remove(gameDeleting);
                db.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        /// Users can change a games information if it is incorrect.
        /// </summary>
        /// <param name="editGame"></param>
        /// <returns></returns>
        internal static int EditGame(GameConsoleViewModel editGame)
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                VideoGame updatingGame = db.VideoGames.First(g => g.GameID == editGame.GameID);
                updatingGame.Title = editGame.Title;
                updatingGame.Publisher = editGame.Publisher;
                updatingGame.Developer = editGame.Developer;
                updatingGame.ReleaseDate = editGame.ReleaseDate;
                updatingGame.BoxArtLink = editGame.BoxArtLink;
                updatingGame.Synopsis = editGame.Synopsis;
                if (editGame.Title != null)
                {
                    //Clear the database with all console entries
                    List<GamesFor> gamesForDeleting = db.GamesFors.Where(p => p.GameID == editGame.GameID).ToList();
                    foreach (GamesFor gamesFor in gamesForDeleting)
                    {
                        db.GamesFors.Remove(gamesFor);
                    }
                    //Add the consoles that were chosen
                    for (int i = 0; i < editGame.chosenConsoles.Count; i++)
                    {
                        if (editGame.chosenConsoles[i] == true)
                        {
                            GamesFor newGamesFor = new GamesFor();
                            newGamesFor.ConsoleID = i + 1;
                            newGamesFor.GameID = editGame.GameID;
                            db.GamesFors.Add(newGamesFor);
                        }
                    }
                    db.SaveChanges();
                    return editGame.GameID;
                }
                return 0;
            }
        }
    }
}