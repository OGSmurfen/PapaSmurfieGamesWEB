using Microsoft.AspNetCore.Mvc;
using PapaSmurfie.Models;
using PapaSmurfie.Models.Models.ViewModels;
using PapaSmurfie.Repository;
using SQLitePCL;
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

        public async Task<IActionResult> Index(string? query)
        {
            Console.WriteLine($"Query: {query}");
            IEnumerable<GameModel> games;
            if (!string.IsNullOrEmpty(query))
            {
                games = await unitOfWork.GamesRepository.GetAllAsync(g => g.GameName.Contains(query));
            }
            else
            {
                games = await unitOfWork.GamesRepository.GetAllAsync();
            }
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

        [HttpGet]
        public IActionResult Search(string query)
        {
            // Assuming `query` is a string; modify as needed
            return RedirectToAction("Index", "Home", new { query });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
