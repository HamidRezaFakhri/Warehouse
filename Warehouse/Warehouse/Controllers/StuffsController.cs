using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class StuffsController : Controller
    {
        private readonly WarehouseContext _context;

        public StuffsController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Stuffs
        public async Task<IActionResult> Index()
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            return View(await _context.Stuffs.ToListAsync());
        }

        // GET: Stuffs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var stuff = await _context.Stuffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stuff == null)
            {
                return NotFound();
            }

            return View(stuff);
        }

        // GET: Stuffs/Create
        public IActionResult Create()
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            return View();
        }

        // POST: Stuffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Code,Description,CreatedDate,InstanceId,Id")] Stuff stuff)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            if (ModelState.IsValid)
            {
                _context.Add(stuff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stuff);
        }

        // GET: Stuffs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var stuff = await _context.Stuffs.FindAsync(id);
            if (stuff == null)
            {
                return NotFound();
            }
            return View(stuff);
        }

        // POST: Stuffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Code,Description,CreatedDate,InstanceId,Id")] Stuff stuff)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            if (id != stuff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stuff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StuffExists(stuff.Id))
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
            return View(stuff);
        }

        // GET: Stuffs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var stuff = await _context.Stuffs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stuff == null)
            {
                return NotFound();
            }

            return View(stuff);
        }

        // POST: Stuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            var stuff = await _context.Stuffs.FindAsync(id);
            _context.Stuffs.Remove(stuff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StuffExists(long id)
        {
            return _context.Stuffs.Any(e => e.Id == id);
        }

        private bool IsUserAdmin()
        {
            HttpContext.Session.TryGetValue("IUI", out byte[] session);
            string currentUserId = session.LastOrDefault().ToString();

            return Convert.ToInt64(currentUserId) == 1;
        }
    }
}
