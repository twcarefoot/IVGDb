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

            return RedirectToAction("Index", game);
        }

        [HttpPost]
        public ActionResult SearchGame(string title)
        {
            VideoGame game = new VideoGameViewModel();

            game = VideoGameViewModel.GetGameByTitle(title);

            return RedirectToAction("Index", game);
        }

        public ActionResult ShowGame(int? gameID)
        {
            if (gameID != null)
            {
                VideoGame game = new VideoGameViewModel();
                game = VideoGameViewModel.GetGameByID((int)gameID);
                return View(game);
            }

            return View();
        }

        public ActionResult Results(String search) 
        {
            if (search != null)
            {
                ViewBag.searchQuery = search;
                List<VideoGame> gameList = VideoGameViewModel.QueryGames(search);
                return View(gameList);
            }
            return View();
        }

        public ActionResult AddNewGame()
        {
            List<Console> console_list = new List<Console>();
            console_list = GameConsoleViewModel.getAllConsoles();
            List<SelectListItem> Consoles = new List<SelectListItem>();

            foreach (var console in console_list)
            {
                SelectListItem item = new SelectListItem();
                item.Text = console.Name;
                item.Value = console.Name;
                Consoles.Add(item);
            }

            return View(Consoles);
        }
    }
}