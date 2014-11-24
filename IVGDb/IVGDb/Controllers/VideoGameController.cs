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
        public ActionResult Index(VideoGameViewModel model)
        {
            if (model != null)
                return View(model);

            return View();
        }

        [HttpPost]
        public ActionResult GetGame(string title)
        {
            VideoGame game = new VideoGameViewModel();

            game = VideoGameViewModel.GetGameByTitle(title);

            return RedirectToAction("ShowGame", game);
        }

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

        public ActionResult ShowAllGames()
        {
            List<VideoGame> list = new List<VideoGame>();
            list = VideoGameViewModel.GetAllGames();
            return View(list);
        }

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

        [HttpPost]
        public ActionResult AddNewGame(GameConsoleViewModel newGame)
        {
            if (ModelState.IsValid)
            {
                int newGameID = GameConsoleViewModel.AddNewGame(newGame);
                return RedirectToAction("ShowGame", new
                {
                    gameID = newGameID,
                });
            }
            return View();
        }

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

        [HttpPost]
        public ActionResult DeleteGame(VideoGame game)
        {
            if (ModelState.IsValid)
            {
                GameConsoleViewModel.DeleteGame(game.GameID);
            }
            return RedirectToAction("Index", "Home");
        }

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