using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyManagement.Models;
using Microsoft.AspNetCore.Http; // Add this for session

namespace PharmacyManagement.Controllers
{
    public class CartsController : Controller
    {
        private readonly PharmacyManagementContext _context;

        public CartsController(PharmacyManagementContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null)
            {
                return RedirectToAction("Index", "LoginC");
            }
            var pharmacyManagementContext = _context.Carts.Include(c => c.Customer).Include(c => c.Medicine).Where(c => c.CustomerId == customerId);
            return View(await pharmacyManagementContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");

            if (customerId == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để sử dụng chức năng này.";
                return RedirectToAction("Index", "LoginC");
            }

            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.MedicineId == id && c.CustomerId == customerId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _context.Carts.Update(existingCartItem);
            }
            else
            {
                var cart = new Cart
                {
                    CustomerId = customerId.Value,
                    MedicineId = id,
                    Quantity = 1
                };
                await _context.Carts.AddAsync(cart);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Thuốc đã được thêm vào giỏ hàng!";
            return RedirectToAction("Index", "Home");
        }

        // POST: Carts/UpdateQuantity
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;
            _context.Carts.Update(cartItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: Carts/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.Medicine)
                .FirstOrDefaultAsync(m => m.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId");
            return View();
        }

        // POST: Carts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,CustomerId,MedicineId,Quantity,Price,AddedAt")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", cart.CustomerId);
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId", cart.MedicineId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", cart.CustomerId);
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId", cart.MedicineId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,CustomerId,MedicineId,Quantity,Price,AddedAt")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", cart.CustomerId);
            ViewData["MedicineId"] = new SelectList(_context.Medicines, "MedicineId", "MedicineId", cart.MedicineId);
            return View(cart);
        }

        // GET: Carts/Delete/5 (For confirmation page)
        public async Task<IActionResult> DeleteConfirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Customer)
                .Include(c => c.Medicine)
                .FirstOrDefaultAsync(m => m.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5 (Actual deletion)
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}