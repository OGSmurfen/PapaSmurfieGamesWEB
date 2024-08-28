using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PapaSmurfie.DataAccess.Repository.RepositoryImpl;
using PapaSmurfie.Models;
using PapaSmurfie.Repository;

namespace PapaSmurfie.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class GameActionsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public GameActionsController(UserManager<IdentityUser> userManager, 
                                    ILogger<HomeController> logger, 
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Play(int gameId)
        {
            GameModel game = await unitOfWork.GamesRepository.GetAsync(g => g.GameId == gameId);


            // when game is in static files
            return View(game);

            // redirect if link is in itch.io
            //return Redirect(game.GameURL);
        }

        [Authorize]
        public async Task<IActionResult> GetGame(int gameId, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Index", "Home");
            }

            GameModel game = await unitOfWork.GamesRepository.GetAsync(g => g.GameId == gameId);
            OwnedGameModel newOwnedGame = new OwnedGameModel
            {
                UserOwnerName = userName,
                GameOwnedId = gameId,
            };

            if (game.GamePrice == 0)
            {
                await unitOfWork.OwnedGamesRepository.CreateAsync(newOwnedGame);
                await unitOfWork.Save();
                TempData["Success"] = "Game successfully added to your collection!";
            }
            return RedirectToAction("Index", "Home", new { gameBought = newOwnedGame });

        }


        public async Task<IActionResult> Details(int gameId)
        {
            GameModel game = await unitOfWork.GamesRepository.GetAsync(g => g.GameId == gameId);
            return View(game);
        }
    }
}
