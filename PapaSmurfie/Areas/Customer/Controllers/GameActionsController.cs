using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapaSmurfie.Models;
using PapaSmurfie.Repository;

namespace PapaSmurfie.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class GameActionsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public GameActionsController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
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
        public async Task<IActionResult> Details(int gameId)
        {
            GameModel game = await unitOfWork.GamesRepository.GetAsync(g => g.GameId == gameId);
            return View(game);
        }
    }
}
