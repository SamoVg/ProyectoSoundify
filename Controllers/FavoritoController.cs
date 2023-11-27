using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoSoundify.Models.dbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoSoundify.ViewModels;

namespace ProyectoSoundify.Controllers
{
    [Authorize]
    public class FavoritoController : Controller
    {
        private readonly SoundifyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoritoController(SoundifyContext Context, UserManager<ApplicationUser> userManager)
        {
            _context = Context;
            _userManager = userManager;
        }

        

        // GET: FavoritoController
        public async Task<ActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            ApplicationUser? Usuario = _context.Users.Where(u => u.Id == user.Id).Include(x => x.IdCancions).FirstOrDefault();
            if (Usuario != null)
            {
                FavoritoViewModel fvm = new FavoritoViewModel
                {
                    User = Usuario,
                    ReturnUrl = Url.Content("~/Favorito/Index")
            };
                return View(fvm);
            }

            return View();

        }

        // GET: FavoritoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FavoritoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FavoritoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int IdCancion, string returnURL)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                ApplicationUser? Usuario = _context.Users.Where(u => u.Id == user.Id).Include(x => x.IdCancions).FirstOrDefault();
                Cancion? cancion = _context.Cancions.Where(m => m.IdCancion == IdCancion).FirstOrDefault();
                if (Usuario!= null && cancion != null)
                {

                    Usuario.IdCancions.Add(cancion);
                    _context.SaveChanges();
                    return Redirect(returnURL);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: FavoritoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FavoritoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: FavoritoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FavoritoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int IdCancion, string returnURL)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                ApplicationUser? Usuario = _context.Users.Where(u => u.Id == user.Id).Include(x => x.IdCancions).FirstOrDefault();
                Cancion? cancion = _context.Cancions.Where(m => m.IdCancion == IdCancion).FirstOrDefault();
                if (Usuario != null && cancion != null)
                {

                    Usuario.IdCancions.Remove(cancion);
                    _context.SaveChanges();
                    return Redirect(returnURL);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch
            {
                return View();
            }
        }
    }
}
