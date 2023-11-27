using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoSoundify.Models;
using ProyectoSoundify.Models.dbModels;
using NAudio.Wave;
using ProyectoSoundify.ViewModels;

namespace ProyectoSoundify.Controllers
{
    [Authorize]
    public class CancionesController : Controller
    {
        private readonly SoundifyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        


        public CancionesController(SoundifyContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DarLike(int IdCancion, string returnURL)
        {
            returnURL = "~/Canciones";
            try
            {
                var user = await _userManager.GetUserAsync(User);
                ApplicationUser? Usuario = _context.Users.Where(u => u.Id == user.Id).Include(x => x.IdCancions).FirstOrDefault();
                Cancion? cancion = _context.Cancions.Where(m => m.IdCancion == IdCancion).FirstOrDefault();
                if (Usuario != null && cancion != null)
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
        // GET: Canciones
        [AllowAnonymous]
        public IActionResult Index()
        {

            var soundifyContext = _context.Cancions.Include(c => c.IdCategoriaNavigation).Include(c => c.IdUsuarioNavigation).OrderByDescending(x => x.Duracion);
           
            CancionViewModel cancionViewModel = new CancionViewModel
            {
                Cancion = soundifyContext,
                
            };

            return View(cancionViewModel);
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cancions == null)
            {
                return NotFound();
            }

            var cancion = await _context.Cancions
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCancion == id);
            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // GET: Canciones/Create
        public IActionResult Create(CancionHR cancion)
        {
            Cancion cancion1 = new Cancion
            {
                Nombre = cancion.Nombre,
                Descripcion = cancion.Descripcion,
                Duracion = cancion.Duracion,
                IdCategoria = cancion.IdCategoria,
                RutaImg = cancion.RutaImg,
                FechaSubida = cancion.FechaSubida,
                IdUsuario = cancion.IdUsuario
            };

            cancion.Categoria = new SelectList(_context.Categoria, "IdCategoria", "NombreCategoria", cancion.IdCategoria);

            
            return View(cancion);
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CancionHR cancion)
        {

            var user = await _userManager.GetUserAsync(User); //Obtiene el ID del usuario Actual
            cancion.FechaSubida = DateTime.UtcNow; 
            cancion.IdUsuario = user.Id;

            if (ModelState.IsValid)
            {
                string? filename = await GuardarFotografiaProductoAsync(cancion.ImagenArchivo);
               

                AudioFileReader audioFileReader = new AudioFileReader("C:\\Users\\AlanV\\Desktop\\ProyectoSoundify\\wwwroot\\Images\\Anuncios\\" + filename);
                double durationInSeconds = audioFileReader.TotalTime.TotalSeconds;
                TimeSpan duration = TimeSpan.FromSeconds(durationInSeconds);
                Cancion cancion1 = new Cancion
                {
                    Nombre = cancion.Nombre,
                    Descripcion = cancion.Descripcion,
                    Duracion = duration,
                    IdCategoria = cancion.IdCategoria,
                    RutaImg = filename,
                    FechaSubida = cancion.FechaSubida,
                    IdUsuario = cancion.IdUsuario
                };
                _context.Add(cancion1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            cancion.Categoria = new SelectList(_context.Categoria, "IdCategoria", "NombreCategoria", cancion.IdCategoria);

            return View(cancion);
        }


        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cancions == null)
            {
                return NotFound();
            }

            var cancion = await _context.Cancions.FindAsync(id);
            if (cancion == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", cancion.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", cancion.IdUsuario);
            return View(cancion);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CancionHR cancion)
        {
            if (id != cancion.IdCancion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Cancion cancion1 = new Cancion
                {
                    Nombre = cancion.Nombre,
                    Descripcion = cancion.Descripcion,
                    Duracion = cancion.Duracion,
                    IdCategoria = cancion.IdCategoria,
                    RutaImg = cancion.RutaImg,
                    FechaSubida = cancion.FechaSubida,
                    IdUsuario = cancion.IdUsuario
                };
                try
                {
                    _context.Update(cancion1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancionExists(cancion.IdCancion))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", cancion.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", cancion.IdUsuario);
            return View(cancion);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cancions == null)
            {
                return NotFound();
            }

            var cancion = await _context.Cancions
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCancion == id);
            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cancions == null)
            {
                return Problem("Entity set 'SoundifyContext.Cancions'  is null.");
            }
            var cancion = await _context.Cancions.FindAsync(id);
            if (cancion != null)
            {
                BorrarFotografiaProducto(cancion.RutaImg);
                _context.Cancions.Remove(cancion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancionExists(int id)
        {
          return (_context.Cancions?.Any(e => e.IdCancion == id)).GetValueOrDefault();
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
