using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoSoundify.Models;
using ProyectoSoundify.Models.dbModels;

namespace ProyectoSoundify.Controllers
{
    public class CancionesController : Controller
    {
        private readonly SoundifyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public CancionesController(SoundifyContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        // GET: Canciones
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var soundifyContext = _context.Cancions.Include(c => c.IdCategoriaNavigation).Include(c => c.IdUsuarioNavigation);
            return View(await soundifyContext.ToListAsync());
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
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria");
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("IdCancion,Nombre,Descripcion,Duracion,IdCategoria,RutaImg,FechaSubida,UserId")] CancionHR cancion)
        {

            var user = await _userManager.GetUserAsync(User); //Obtiene el ID del usuario Actual

            cancion.FechaSubida = DateTime.UtcNow; 
            cancion.IdUsuario = user.Id;

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
        public async Task<IActionResult> Edit(int id, [Bind("IdCancion,Nombre,Descripcion,Duracion,IdCategoria,RutaImg,FechaSubida,IdUsuario")] CancionHR cancion)
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
                _context.Cancions.Remove(cancion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancionExists(int id)
        {
          return (_context.Cancions?.Any(e => e.IdCancion == id)).GetValueOrDefault();
        }
    }
}
