using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoSoundify.Models;
using ProyectoSoundify.Models.dbModels;

namespace ProyectoSoundify.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AnunciosController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SoundifyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnunciosController(SoundifyContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Anuncios
        public IActionResult Index()
        {
            IEnumerable<Anuncio> lstAnuncios = _context.Anuncios.Include(a => a.IdUsuarioNavigation).ToList();
            return View(lstAnuncios);
        }

        // GET: Anuncios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Anuncios == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAnuncio == id);
            if (anuncio == null)
            {
                return NotFound();
            }

            return View(anuncio);
        }

        // GET: Anuncios/Create
        public IActionResult Create(AnuncioHR anuncio)
        {
            Anuncio anuncio1 = new Anuncio();
           

            return View(anuncio);
        }

        // POST: Anuncios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AnuncioHR anuncio)
        {
            var user = await _userManager.GetUserAsync(User); //Obtiene el ID del usuario Actual
            anuncio.IdUsuario = user.Id;
            string? filename = await GuardarFotografiaProductoAsync(anuncio.ImagenArchivo);
            anuncio.RutaImgAnuncio = filename;
            if (ModelState.IsValid)

            {

                Anuncio anuncio1 = new Anuncio
                {
                    TituloAnuncio = anuncio.TituloAnuncio,
                    RutaImgAnuncio = filename,
                    IdUsuario = anuncio.IdUsuario,
                   
                };
                _context.Anuncios.Add(anuncio1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anuncio);
        }

        // GET: Anuncios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Anuncios == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios.FindAsync(id);
            if (anuncio == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", anuncio.IdUsuario);
            return View(anuncio);
        }

        // POST: Anuncios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnuncioHR anuncio)
        {
            if (id != anuncio.IdAnuncio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Anuncio anuncio1 = new Anuncio
                {
                    IdAnuncio = anuncio.IdAnuncio,
                    TituloAnuncio = anuncio.TituloAnuncio,
                    RutaImgAnuncio = anuncio.RutaImgAnuncio,
                    IdUsuario = anuncio.IdUsuario
                };
                try
                {
                    _context.Update(anuncio1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnuncioExists(anuncio.IdAnuncio))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", anuncio.IdUsuario);
            return View(anuncio);
        }

        // GET: Anuncios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Anuncios == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios
                .Include(a => a.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdAnuncio == id);
            if (anuncio == null)
            {
                return NotFound();
            }

            return View(anuncio);
        }

        // POST: Anuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Anuncios == null)
            {
                return Problem("Entity set 'SoundifyContext.Anuncios'  is null.");
            }
            var anuncio = await _context.Anuncios.FindAsync(id);
            if (anuncio != null)
            {
                _context.Anuncios.Remove(anuncio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnuncioExists(int id)
        {
          return (_context.Anuncios?.Any(e => e.IdAnuncio == id)).GetValueOrDefault();
        }

        public async Task<string?> ReemplazarFotografiaAsync(IFormFile? file, string? fileToReplace)
        {
            if (file != null)
            {
                string? newFileName = await GuardarFotografiaProductoAsync(file);
                if (newFileName != null)
                {
                    BorrarFotografiaProducto(fileToReplace);
                    return newFileName;
                }
            }
            return null;
        }

        public async Task<string?> GuardarFotografiaProductoAsync(IFormFile? file)
        {
            if (file != null)
            {
                try
                {
                    string folder = "Images/Anuncios/";
                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder + fileName);

                    using (FileStream stream = new FileStream(serverFolder, FileMode.Create))
                    {
                        //Copies data from entity.file to stream
                        await file.CopyToAsync(stream);
                    }
                    return fileName;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public bool BorrarFotografiaProducto(string? fileName)
        {
            if (fileName != null)
            {
                try
                {
                    string folder = "Images/Anuncios/" + fileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    FileInfo fileInfo = new FileInfo(serverFolder);
                    fileInfo.Delete();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

    }

}
