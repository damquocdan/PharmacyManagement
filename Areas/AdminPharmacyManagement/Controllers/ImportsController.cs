using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Models;

namespace PharmacyManagement.Areas.AdminPharmacyManagement.Controllers
{
    [Area("AdminPharmacyManagement")]
    public class ImportsController : Controller
    {
        private readonly PharmacyManagementContext _context;

        public ImportsController(PharmacyManagementContext context)
        {
            _context = context;
        }

        // GET: AdminPharmacyManagement/Imports
        public async Task<IActionResult> Index()
        {
            var pharmacyManagementContext = _context.Imports.Include(i => i.Medicine);
            return View(await pharmacyManagementContext.ToListAsync());
        }

        // GET: AdminPharmacyManagement/Imports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var import = await _context.Imports
                .Include(i => i.Medicine)
                .FirstOrDefaultAsync(m => m.ImportId == id);
            if (import == null)
            {
                return NotFound();
            }

            return View(import);
        }

        // GET: AdminPharmacyManagement/Imports/Create
        public IActionResult Create()
        {
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId");
            return View();
        }

        // POST: AdminPharmacyManagement/Imports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportId,SupplierName,ImportDate,MedicineId,Quantity,CostPrice")] Import import)
        {
            if (ModelState.IsValid)
            {
                _context.Add(import);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId", import.MedicineId);
            return View(import);
        }

        // GET: AdminPharmacyManagement/Imports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var import = await _context.Imports.FindAsync(id);
            if (import == null)
            {
                return NotFound();
            }
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId", import.MedicineId);
            return View(import);
        }

        // POST: AdminPharmacyManagement/Imports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImportId,SupplierName,ImportDate,MedicineId,Quantity,CostPrice")] Import import)
        {
            if (id != import.ImportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(import);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportExists(import.ImportId))
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
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId", import.MedicineId);
            return View(import);
        }

        // GET: AdminPharmacyManagement/Imports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var import = await _context.Imports
                .Include(i => i.Medicine)
                .FirstOrDefaultAsync(m => m.ImportId == id);
            if (import == null)
            {
                return NotFound();
            }

            return View(import);
        }

        // POST: AdminPharmacyManagement/Imports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var import = await _context.Imports.FindAsync(id);
            if (import != null)
            {
                _context.Imports.Remove(import);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportExists(int id)
        {
            return _context.Imports.Any(e => e.ImportId == id);
        }
    }
}
