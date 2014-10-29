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
    }
}