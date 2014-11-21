using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVGDb.Models;

namespace IVGDb.Models
{
    public class GameConsoleViewModel : VideoGame
    {
        public List<bool> chosenConsoles { get; set; } //Attribute for chosen game consoles for new game entry

        public List<Console> consolesList { get; set; } //Attribute for chosen game consoles for new game entry

        private static ivgdb_Entities db = new ivgdb_Entities();

        public static List<Console> getAllConsoles()
        {
            using (ivgdb_Entities db = new ivgdb_Entities())
            {
                return db.Consoles.ToList();
            }
        }

        

        //Return the ID of the newly added game
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

    }
}