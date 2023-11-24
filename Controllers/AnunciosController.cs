using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private readonly SoundifyContext _context;

        public AnunciosController(SoundifyContext context)
        {
            _context = context;
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
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Anuncios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAnuncio,TituloAnuncio,RutaImgAnuncio,IdUsuario")] AnuncioHR anuncio)
        {
            if (ModelState.IsValid)
            {
                Anuncio anuncio1 = new Anuncio
                {
                    IdAnuncio = anuncio.IdAnuncio,
                    TituloAnuncio = anuncio.TituloAnuncio,
                    RutaImgAnuncio = anuncio.RutaImgAnuncio,
                    IdUsuario = anuncio.IdUsuario
                };
                _context.Anuncios.Add(anuncio1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Users, "Id", "Id", anuncio.IdUsuario);
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


    }
}
