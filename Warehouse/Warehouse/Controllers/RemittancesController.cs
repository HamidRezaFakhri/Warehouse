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
    public class RemittancesController : Controller
    {
        private readonly WarehouseContext _context;

        public RemittancesController(WarehouseContext context)
        {
            _context = context;
        }

        // GET: Remittances
        public async Task<IActionResult> Index()
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }


            var warehouseContext = _context.Remittances.Include(r => r.Store).Include(r => r.User);
            return View(await warehouseContext.ToListAsync());
        }

        // GET: Remittances/Details/5
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

            var remittance = await _context.Remittances
                .Include(r => r.Store)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (remittance == null)
            {
                return NotFound();
            }

            return View(remittance);
        }

        // GET: Remittances/Create
        public IActionResult Create()
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Password");
            return View();
        }

        // POST: Remittances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,RemittanceType,InDate,StoreId,UserId,Description,InstanceId,Id")] Remittance remittance)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            HttpContext.Session.TryGetValue("IUI", out byte[] session);
            string currentUserId = session.LastOrDefault().ToString();

            remittance.UserId = Convert.ToInt64(currentUserId);

            remittance.InDate = DateTime.Now;
            remittance.StoreId = 1;

            if (ModelState.IsValid)
            {
                _context.Add(remittance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Name", remittance.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Password", remittance.UserId);
            return View(remittance);
        }

        // GET: Remittances/Edit/5
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

            var remittance = await _context.Remittances.FindAsync(id);
            if (remittance == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Name", remittance.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Password", remittance.UserId);
            return View(remittance);
        }

        // POST: Remittances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Code,RemittanceType,InDate,StoreId,UserId,Description,InstanceId,Id")] Remittance remittance)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            if (id != remittance.Id)
            {
                return NotFound();
            }

            HttpContext.Session.TryGetValue("IUI", out byte[] session);
            string currentUserId = session.LastOrDefault().ToString();

            remittance.UserId = Convert.ToInt64(currentUserId);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(remittance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RemittanceExists(remittance.Id))
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
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Name", remittance.StoreId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Password", remittance.UserId);
            return View(remittance);
        }

        // GET: Remittances/Delete/5
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

            var remittance = await _context.Remittances
                .Include(r => r.Store)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (remittance == null)
            {
                return NotFound();
            }

            return View(remittance);
        }

        // POST: Remittances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (!IsUserAdmin())
            {
                return RedirectToAction("Error", "Home");
            }

            var remittance = await _context.Remittances.FindAsync(id);
            _context.Remittances.Remove(remittance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RemittanceExists(long id)
        {
            return _context.Remittances.Any(e => e.Id == id);
        }

        private bool IsUserAdmin()
        {
            HttpContext.Session.TryGetValue("IUI", out byte[] session);
            string currentUserId = session.LastOrDefault().ToString();

            return Convert.ToInt64(currentUserId) == 1;
        }
    }
}
