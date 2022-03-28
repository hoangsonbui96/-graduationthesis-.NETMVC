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

namespace MyStore.Controllers
{
    [Authorize(Roles = SystemFunctions.ManageAccount)]
    public class AccountsController : Controller
    {
        private readonly MyStoreContext _context;

        public AccountsController(MyStoreContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Account.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(string id )
        {
            if (id == null)
            {
                return NotFound();

            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Username == id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public async Task<IActionResult> Create()
        {
            var Rolecodelist = await _context.RoleMaster
                .AsNoTracking()
                .Select(x => new { x.RoleCode, x.RoleName }).ToListAsync();
            var takerolecodelist = new SelectList(Rolecodelist, "RoleCode", "RoleCode");
            ViewData["RoleCode"] = takerolecodelist;
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Fullname,EmailAdress,PhoneNumber,RoleCode")] Account account)
        {

            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var Rolecodelist = await _context.RoleMaster
                    .AsNoTracking()
                    .Select(x => new { x.RoleCode, x.RoleName }).ToListAsync();
            var takerolecodelist = new SelectList(Rolecodelist, "RoleCode", "RoleCode");
            ViewData["RoleCode"] = takerolecodelist;

            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            var Rolecodelist = await _context.RoleMaster
                .AsNoTracking()
                .Select(x => new { x.RoleCode, x.RoleName }).ToListAsync();
            var takerolecodelist = new SelectList(Rolecodelist, "RoleCode", "RoleCode");
            ViewData["RoleCode"] = takerolecodelist;
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,Fullname,EmailAdress,PhoneNumber,RoleCode")] Account account)
        {
            if (id != account.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Username))
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

            var Rolecodelist = await _context.RoleMaster
                .AsNoTracking()
                .Select(x => new { x.RoleCode, x.RoleName }).ToListAsync();
            var takerolecodelist = new SelectList(Rolecodelist, "RoleCode", "RoleCode");
            ViewData["RoleCode"] = takerolecodelist;


            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Username == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
            return _context.Account.Any(e => e.Username == id);
        }
    }
}
