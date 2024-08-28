using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PapaSmurfie.Models;
using PapaSmurfie.Repository;
using PapaSmurfie.Utility;

namespace PapaSmurfie.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GamesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public GamesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<GameModel> games = await unitOfWork.GamesRepository.GetAllAsync();
            return View(games);
        }
    }
}
