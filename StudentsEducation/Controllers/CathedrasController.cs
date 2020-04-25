using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Infrastructure.Data;

namespace StudentsEducation.Web.Controllers
{
    public class CathedrasController : Controller
    {
        private readonly EducationDbContext _context;

        public CathedrasController(EducationDbContext context)
        {
            _context = context;
        }

        // GET: Cathedras
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cathedras.ToListAsync());
        }

        // GET: Cathedras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cathedra = await _context.Cathedras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cathedra == null)
            {
                return NotFound();
            }

            return View(cathedra);
        }

        // GET: Cathedras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cathedras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,MainPhoneNumber,SecondPhoneNumber,Id")] Cathedra cathedra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cathedra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cathedra);
        }

        // GET: Cathedras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cathedra = await _context.Cathedras.FindAsync(id);
            if (cathedra == null)
            {
                return NotFound();
            }
            return View(cathedra);
        }

        // POST: Cathedras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,MainPhoneNumber,SecondPhoneNumber,Id")] Cathedra cathedra)
        {
            if (id != cathedra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cathedra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CathedraExists(cathedra.Id))
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
            return View(cathedra);
        }

        // GET: Cathedras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cathedra = await _context.Cathedras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cathedra == null)
            {
                return NotFound();
            }

            return View(cathedra);
        }

        // POST: Cathedras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cathedra = await _context.Cathedras.FindAsync(id);
            _context.Cathedras.Remove(cathedra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CathedraExists(int id)
        {
            return _context.Cathedras.Any(e => e.Id == id);
        }
    }
}
