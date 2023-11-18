using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoSoundify.Models.dbModels;

namespace ProyectoSoundify.Controllers
{
    public class CancionesController : Controller
    {
        private readonly SoundifyContext _context;

        public CancionesController(SoundifyContext context)
        {
            _context = context;
        }

        // GET: Canciones
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
        public async Task<IActionResult> Create([Bind("IdCancion,Nombre,Descripcion,Duracion,IdCategoria,RutaImg,FechaSubida,IdUsuario")] Cancion cancion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", cancion.IdCategoria);
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", cancion.IdUsuario);
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
        public async Task<IActionResult> Edit(int id, [Bind("IdCancion,Nombre,Descripcion,Duracion,IdCategoria,RutaImg,FechaSubida,IdUsuario")] Cancion cancion)
        {
            if (id != cancion.IdCancion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancion);
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
