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
    public class RemittanceStuffsController : Controller
    {
        private readonly WarehouseContext _context;

        private static long _remittanceId;

        public RemittanceStuffsController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: RemittanceStuffs
        public async Task<IActionResult> Index(long Id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            _remittanceId = Id;

            var warehouseContext = _context.RemittanceStuffs.Include(r => r.Remittance).Include(r => r.Stuff);
            return View(await warehouseContext.ToListAsync());
        }

        // GET: RemittanceStuffs/Details/5
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

            var remittanceStuff = await _context.RemittanceStuffs
                .Include(r => r.Remittance)
                .Include(r => r.Stuff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (remittanceStuff == null)
            {
                return NotFound();
            }

            return View(remittanceStuff);
        }

        // GET: RemittanceStuffs/Create
        public IActionResult Create()
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            ViewData["RemittanceId"] = new SelectList(_context.Remittances, "Id", "Code");
            ViewData["StuffId"] = new SelectList(_context.Stuffs, "Id", "Code");
            return View();
        }

        // POST: RemittanceStuffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RemittanceId,StuffId,Count,InstanceId,Id")] RemittanceStuff remittanceStuff)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            remittanceStuff.RemittanceId = _remittanceId;
            remittanceStuff.Count = remittanceStuff.Count <= 0 ? 1 : remittanceStuff.Count;

            if (ModelState.IsValid)
            {
                _context.Add(remittanceStuff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RemittanceId"] = new SelectList(_context.Remittances, "Id", "Code", remittanceStuff.RemittanceId);
            ViewData["StuffId"] = new SelectList(_context.Stuffs, "Id", "Code", remittanceStuff.StuffId);
            return View(remittanceStuff);
        }

        // GET: RemittanceStuffs/Edit/5
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

            var remittanceStuff = await _context.RemittanceStuffs.FindAsync(id);
            if (remittanceStuff == null)
            {
                return NotFound();
            }
            ViewData["RemittanceId"] = new SelectList(_context.Remittances, "Id", "Code", remittanceStuff.RemittanceId);
            ViewData["StuffId"] = new SelectList(_context.Stuffs, "Id", "Code", remittanceStuff.StuffId);
            return View(remittanceStuff);
        }

        // POST: RemittanceStuffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RemittanceId,StuffId,Count,InstanceId,Id")] RemittanceStuff remittanceStuff)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            if (id != remittanceStuff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(remittanceStuff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemittanceStuffExists(remittanceStuff.Id))
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
            ViewData["RemittanceId"] = new SelectList(_context.Remittances, "Id", "Code", remittanceStuff.RemittanceId);
            ViewData["StuffId"] = new SelectList(_context.Stuffs, "Id", "Code", remittanceStuff.StuffId);
            return View(remittanceStuff);
        }

        // GET: RemittanceStuffs/Delete/5
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

            var remittanceStuff = await _context.RemittanceStuffs
                .Include(r => r.Remittance)
                .Include(r => r.Stuff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (remittanceStuff == null)
            {
                return NotFound();
            }

            return View(remittanceStuff);
        }

        // POST: RemittanceStuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            var remittanceStuff = await _context.RemittanceStuffs.FindAsync(id);
            _context.RemittanceStuffs.Remove(remittanceStuff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemittanceStuffExists(long id)
        {
            return _context.RemittanceStuffs.Any(e => e.Id == id);
        }

        private bool IsUserAdmin()
        {
            HttpContext.Session.TryGetValue("IUI", out byte[] session);
            string currentUserId = session.LastOrDefault().ToString();

            return Convert.ToInt64(currentUserId) == 1;
        }
    }
}
