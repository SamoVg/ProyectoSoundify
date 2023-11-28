using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PlaylistsController : Controller
    {
        private readonly SoundifyContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PlaylistsController(SoundifyContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize]
        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); //Obtiene el ID del usuario Actual

            var soundifyContext = _context.Playlists.Include(p => p.IdUserNavigation).Where(d=>d.IdUser == user.Id);
            return View(await soundifyContext.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdPlaylist == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPlaylist,IdUser,FechaCreacion,NombrePlaylist")] PlaylistHR playlist)
        {
            
            var user = await _userManager.GetUserAsync(User); //Obtiene el ID del usuario Actual

            playlist.FechaCreacion = DateTime.UtcNow;
            playlist.IdUser = user.Id; 
            Playlist playlist1 = new Playlist
                {
                IdUser= playlist.IdUser,
                FechaCreacion = playlist.FechaCreacion,
                NombrePlaylist = playlist.NombrePlaylist
            };
            if (ModelState.IsValid)
            {
                _context.Add(playlist1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", playlist.IdUser);
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlaylist,IdUser,FechaCreacion,NombrePlaylist")] Playlist playlist)
        {
            if (id != playlist.IdPlaylist)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.IdPlaylist))
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
            ViewData["IdUser"] = new SelectList(_context.Users, "Id", "Id", playlist.IdUser);
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Playlists == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdPlaylist == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Playlists == null)
            {
                return Problem("Entity set 'SoundifyContext.Playlists'  is null.");
            }
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(int id)
        {
          return (_context.Playlists?.Any(e => e.IdPlaylist == id)).GetValueOrDefault();
        }
    }
}
