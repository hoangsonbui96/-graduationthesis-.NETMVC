using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;
using Microsoft.AspNetCore.Identity;


namespace MyStore.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MyStoreContext _context;

        public CustomersController(MyStoreContext context)
        {

            _context = context;
        }

        [Authorize(Roles = SystemFunctions.ManageCustomer)]
        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customer.ToListAsync());
        }

        //[Authorize(Roles = SystemFunctions.ManageCustomer)]
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string phone)
        {
            string gettedNum = User.Claims.FirstOrDefault(i => i.Type == "CustomerPhone").Value;
            if (phone == null)
            {
                return NotFound();
            }


            var customer = await _context.Customer
                                .FirstOrDefaultAsync(m => m.CustomerPhone == phone);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {

                _context.Add(customer);

                await _context.SaveChangesAsync();
                var customerId = customer.Id;
                return RedirectToAction(nameof(CongraCreated), new { id = customerId });
            }
            return View();
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id, string phone)
        {
            string gettedNum = User.Claims.FirstOrDefault(i => i.Type == "CustomerPhone").Value;

            if (id == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Manage_Customer"))
            {
                var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }


            else if(phone == gettedNum)
            {
                var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }

            return NotFound();

        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            string gettedNum = User.Claims.FirstOrDefault(i => i.Type == "CustomerPhone").Value;
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (User.IsInRole("Manage_Customer"))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.Id))
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
            }
            else
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Details), new { id = id, phone = gettedNum });

                }

            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string gettedNum = User.Claims.FirstOrDefault(i => i.Type == "CustomerPhone").Value;
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);

        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

        public IActionResult CongraCreated(int Id)
        {
            ViewBag.ID = Id;
            return View();
        }

        public IActionResult CustomerLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CustomerLogin(CustomerLoginModel model, string returnUrl)
        {
            var validUser = await _context.Customer.FirstOrDefaultAsync(i => i.CustomerPhone == model.CustomerPhoneNumber);
            if (validUser == null)
            {
                ModelState.AddModelError(string.Empty, "Số điện thoại chưa đăng ký hoặc nhập sai số điện thoại xin vui lòng thử lại hoặc đăng ký số điện thoại");
            }
            if (ModelState.IsValid)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, validUser.CustomerName));
                identity.AddClaim(new Claim(ClaimTypes.MobilePhone, model.CustomerPhoneNumber));
                identity.AddClaim(new Claim(CustomClaim.Fullname, validUser.CustomerName));
                identity.AddClaim(new Claim(CustomClaim.Address, validUser.ShippingAddress));
                identity.AddClaim(new Claim("CustomerPhone", model.CustomerPhoneNumber));

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
