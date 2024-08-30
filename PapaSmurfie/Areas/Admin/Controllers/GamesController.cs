using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using PapaSmurfie.Models;
using PapaSmurfie.Models.Models;
using PapaSmurfie.Models.Models.ViewModels;
using PapaSmurfie.Repository;
using PapaSmurfie.Repository.IRepository;
using PapaSmurfie.Utility;
using System.IO;
using System.IO.Compression;

namespace PapaSmurfie.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class GamesController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUnitOfWork unitOfWork;
        public GamesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<GameModel> games = await unitOfWork.GamesRepository.GetAllAsync();
            return View(games);
        }

        public IActionResult AddNewGame()
        {
            return View(new AddGameVM() { Game = new GameModel(), UploadFileModel = new UploadGameFilesModel() });
        }

        [HttpPost]
        [RequestSizeLimit(1073741824)] // 1 GB
        [ActionName("AddNewGame")]
        public async Task<IActionResult> AddNewGamePOST(AddGameVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.UploadFileModel.GameFile != null && model.UploadFileModel.GameImage != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;
                    var unzipURI = Path.Combine(wwwRootPath, @"games\" + model.Game.GameName);

                    var gameZipPathTemp = Path.Combine(wwwRootPath, @"games\" + model.Game.GameName );
                    using (var gameFileStream = new FileStream(gameZipPathTemp + Path.GetExtension(model.UploadFileModel.GameFile.FileName), FileMode.Create))
                    {
                        model.UploadFileModel.GameFile.CopyTo(gameFileStream);
                    }

                    using(var imageStream = new FileStream(Path.Combine(wwwRootPath, @"images\games\" + model.Game.GameName + Path.GetExtension(model.UploadFileModel.GameImage.FileName)), FileMode.Create))
                    {
                        model.UploadFileModel.GameImage.CopyTo(imageStream);
                    }

                   
                    ZipFile.ExtractToDirectory(gameZipPathTemp + Path.GetExtension(model.UploadFileModel.GameFile.FileName), gameZipPathTemp);

                    System.IO.File.Delete(gameZipPathTemp + Path.GetExtension(model.UploadFileModel.GameFile.FileName));

                    // path we save in db is relative to wwwroot
                    // we do not need full path like the ZipFile extractor does
                    // creating game record in database:
                    model.Game.GamePhotoURI = @"images\games\" + model.Game.GameName + Path.GetExtension(model.UploadFileModel.GameImage.FileName);
                    model.Game.GameURL = @"games\" + model.Game.GameName + @"\" + model.Game.GameName + @"\index.html";
                    await unitOfWork.GamesRepository.CreateAsync(model.Game);
                    await unitOfWork.Save();

                    TempData["Message"] = "Game sucessfully added!";
                    return RedirectToAction("Index", "Games");
                }
                else
                {
                    TempData["Message"] = "Error with provided game files";
                    return View();
                }
                
            }
            TempData["Message"] = "Form not filled correctly!";
            return View();
        }

    }
}
