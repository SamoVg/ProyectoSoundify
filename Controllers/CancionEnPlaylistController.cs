using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoSoundify.Models.dbModels;

namespace ProyectoSoundify.Controllers
{
    public class CancionEnPlaylistController : Controller
    {
        private readonly SoundifyContext _context;
        private readonly UserManager<ApplicationUser> _user;

        public CancionEnPlaylistController(SoundifyContext context, UserManager<ApplicationUser> user)
        {
            _context = context;
            _user = user;
        }


        // GET: CancionEnPlaylist
        public ActionResult Index()
        {
            return View();
        }

        // GET: CancionEnPlaylist/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CancionEnPlaylist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CancionEnPlaylist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CancionEnPlaylist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CancionEnPlaylist/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CancionEnPlaylist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CancionEnPlaylist/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
