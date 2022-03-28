using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using System.Security.Claims;
namespace MyStore.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly MyStoreContext _context;

        public OrderDetailsController(MyStoreContext context)
        {
            _context = context;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index(string GetUserPhone)
        {
            string customerPhone = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.MobilePhone).Value;

            if (User.IsInRole("Manage_Customer"))
            {
                var myStoreContext = _context.OrderDetail.ToListAsync();
                return View(await myStoreContext);
            }
            else
            {
                var myStoreContext = _context.OrderDetail.Where(i => i.CustomerPhone == customerPhone);

                return View(await myStoreContext.ToListAsync());
            }

        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create(int id, string name, float price, float bonuspercent)
        {
            ViewBag.ID = id;
            ViewBag.Name = name;
            ViewBag.Price = price;
            ViewBag.BonusPercentage = bonuspercent;

            //ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    _context.Add(orderDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "OrderDetails");
                }
                else
                {
                    _context.Add(orderDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("CustomerLogin", "Customers");
                }

            }
            //ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderDetail.ProductId);
            return NotFound();
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id, int productid)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .FirstOrDefaultAsync(m => m.OrderId == id && m.ProductId == productid);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int productid)
        {
            var orderDetail = await _context.OrderDetail
                .FirstOrDefaultAsync(m => m.OrderId == id && m.ProductId == productid);
            _context.OrderDetail.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.OrderId == id);
        }
    }
}
