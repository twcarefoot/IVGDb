using IVGDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IVGDb.Controllers
{
    public class VideoGameController : Controller
    {
        /// <summary>
        /// Main page index method.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index(VideoGameViewModel model)
        {
            if (model != null)
                return View(model);

            return View();
        }

        /// <summary>
        /// Get Game.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetGame(string title)
        {
            VideoGame game = new VideoGameViewModel();

            game = VideoGameViewModel.GetGameByTitle(title);

            return RedirectToAction("ShowGame", game);
        }

        /// <summary>
        /// Displays the game on a game page.
        /// Validation for parameters.
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public ActionResult ShowGame(int? gameID)
        {
            if (gameID != null)
            {
                VideoGame newgame = new GameConsoleViewModel();
                newgame = GameConsoleViewModel.GetGameByID((int)gameID);
                if (newgame == null)
                {
                    return View();
                }
                newgame.GamesFors = GameConsoleViewModel.GetGameConsoles((int)gameID);
                return View(newgame);
            }

            return View();
        }

        /// <summary>
        /// Displays a list of all games presently added in the database.
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAllGames()
        {
            List<VideoGame> list = new List<VideoGame>();
            list = VideoGameViewModel.GetAllGames();
            return View(list);
        }
        
        /// <summary>
        /// Queries the database based of of URL parameters.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult Results(String search)
        {
            if (search != null && search.Length > 1)
            {
                List<VideoGame> gameList = VideoGameViewModel.QueryGames(search);   
                ViewBag.searchQuery = search;
                return View(gameList);
            }
            return View();
        }

        
        /// <summary>
        /// Method to add a new game to the database.
        /// Users must be logged in to do so.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddNewGame()
        {
            if (User.Identity.Name.Length == 0)
            {
                return RedirectToAction("Register", "User", new { unregistered = true });
            }
            GameConsoleViewModel vgViewModel = new GameConsoleViewModel();
            vgViewModel.consolesList = GameConsoleViewModel.getAllConsoles();
            return View(vgViewModel);
        }

        /// <summary>
        /// Post method to add new game to database.
        /// Games require a title only.
        /// Other fields can be null because the purpose of the website it to gain as much knowledge
        /// and continually add the data from users.
        /// </summary>
        /// <param name="newGame"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddNewGame(GameConsoleViewModel newGame)
        {
            if (ModelState.IsValid)
            {
                if (newGame.Title == null)
                {
                    ModelState.AddModelError("GameNameErr", "Games require a title.");
                    return RedirectToAction("AddNewGame", "VideoGame");
                }
                int newGameID = GameConsoleViewModel.AddNewGame(newGame);
                return RedirectToAction("ShowGame", new
                {
                    gameID = newGameID,
                });
            }
            return View();
        }

        /// <summary>
        /// Post method for editing a game.
        /// </summary>
        /// <param name="newGame"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditGame(GameConsoleViewModel newGame)
        {
            if (ModelState.IsValid)
            {
                int newGameID = GameConsoleViewModel.EditGame(newGame);
                return RedirectToAction("ShowGame", new
                {
                    gameID = newGameID,
                });
            }
            return View();
        }


        /// <summary>
        /// Displays the edit game form. Prepopulates the field based off of the game data.
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public ActionResult EditGame(int? gameID)
        {
            if (gameID != null) {
                GameConsoleViewModel vgViewModel = GameConsoleViewModel.CreateNewGameConsoleViewModel(GameConsoleViewModel.GetGameByID((int)gameID));
                vgViewModel.consolesList = GameConsoleViewModel.getAllConsoles();
                vgViewModel.GamesFors = GameConsoleViewModel.GetGameConsoles((int)gameID);
                return View(vgViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Deletes the game from the database.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteGame(VideoGame game)
        {
            if (ModelState.IsValid)
            {
                GameConsoleViewModel.DeleteGame(game.GameID);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Displays the delete game form.
        /// Asks the user for absolute confirmation.
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public ActionResult DeleteGame(int? gameID)
        {
            if(gameID != null){
                VideoGame game = new VideoGameViewModel();
                game = VideoGameViewModel.GetGameByID((int)gameID);
                game.GamesFors = VideoGameViewModel.GetGameConsoles((int)gameID);
                return View(game);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}