using Microsoft.AspNetCore.Mvc;
using PapaSmurfie.Models;
using PapaSmurfie.Repository;
using System.Diagnostics;

namespace PapaSmurfie.Controllers
{
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
            return View(games);
        }


        public async Task<IActionResult> Details(int gameId)
        {
            GameModel game = await unitOfWork.GamesRepository.GetAsync(g => g.GameId == gameId);
            return View(game);
        }

        public async Task<IActionResult> Play(int gameId)
        {
            //this is the one for embedded:
            //GameModel game = await unitOfWork.GamesRepository.GetAsync(g => g.GameId == gameId);
            //return View(game);

            //for now redirect, must change to embedded later !!
            GameModel game = await unitOfWork.GamesRepository.GetAsync(g => g.GameId == gameId);
            return Redirect(game.GameURL);
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
