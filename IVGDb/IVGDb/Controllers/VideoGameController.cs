﻿using IVGDb.Models;
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

        public ActionResult ShowGame(VideoGameViewModel game, int? gameID)
        {
            if (gameID == null && game != null)
            {
                VideoGame newgame = new VideoGameViewModel();
                newgame = VideoGameViewModel.GetGameByTitle(game.Title);
                return View(newgame);
            }
            if (gameID != null)
            {
                VideoGame newgame = new VideoGameViewModel();
                newgame = VideoGameViewModel.GetGameByID((int)gameID);
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

                VideoGame game = new VideoGameViewModel();
                game = VideoGameViewModel.GetGameByID(newGameID);
                return RedirectToAction("ShowGame", game);
            }
            return View();
        }

    }
}