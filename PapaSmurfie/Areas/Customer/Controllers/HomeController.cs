using Microsoft.AspNetCore.Mvc;
using PapaSmurfie.Models;
using PapaSmurfie.Models.Models.ViewModels;
using PapaSmurfie.Repository;
using System.Diagnostics;

namespace PapaSmurfie.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GameModel> games = await unitOfWork.GamesRepository.GetAllAsync();
            IEnumerable<OwnedGameModel> gamesWithOwners = await unitOfWork.OwnedGamesRepository.GetAllAsync();
            GameVM gameVM = new GameVM
            {
                Games = games,
                GamesWithOwners = gamesWithOwners
            };


            return View(gameVM);
        }

        public async Task<IActionResult> Library(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {

            }
            IEnumerable<OwnedGameModel> allOwnedGames = await unitOfWork.OwnedGamesRepository.GetAllAsync(g => g.UserOwnerName == userName);
            List<GameModel> gamesToDisplay = new List<GameModel>();
            foreach (var ownedGame in allOwnedGames)
            {
                gamesToDisplay.Add(await unitOfWork.GamesRepository.GetAsync(game => game.GameId == ownedGame.GameOwnedId));
            }


            return View(gamesToDisplay);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
