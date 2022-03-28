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
    [Authorize(Roles = SystemFunctions.ManageRolePermision)]
    public class RolePermisionsController : Controller
    {
        private readonly MyStoreContext _context;

        public RolePermisionsController(MyStoreContext context)
        {
            _context = context;
        }

        // GET: RolePermisions
        public async Task<IActionResult> Index()
        {
            return View(await _context.RolePermision.ToListAsync());
        }

        // GET: RolePermisions/Details/5
        public async Task<IActionResult> Details(string rolecode, string functionrole)
        {
            if (string.IsNullOrWhiteSpace(rolecode) || string.IsNullOrWhiteSpace(functionrole))
            {
                return NotFound();
            }

            var rolePermision = await _context.RolePermision
                .FirstOrDefaultAsync(m => m.RoleCode == rolecode && m.FunctionRole == functionrole);
            if (rolePermision == null)
            {
                return NotFound();
            }

            return View(rolePermision);
        }

        // GET: RolePermisions/Create
        public async Task<IActionResult>  Create()
        {
            var Rolecodelist = await _context.RoleMaster
                .AsNoTracking()
                .Select(x => new { x.RoleCode, x.RoleName }).ToListAsync();
            var takerolecodelist = new SelectList(Rolecodelist, "RoleCode", "RoleCode");
            ViewData["RoleCode"] = takerolecodelist;


            ViewData["FunctionCode"] = new SelectList(BaseOptions.GetOptions<SystemFunctions>());
            return View();
        }

        // POST: RolePermisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleCode,FunctionRole")] RolePermision rolePermision)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolePermision);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var Rolecodelist = await _context.RoleMaster.AsNoTracking().Select(x => new { x.RoleCode }).ToListAsync();
            var takerolecodelist = new SelectList(Rolecodelist, "RoleCode");
            ViewData["RoleCode"] = takerolecodelist;


            ViewData["FunctionCode"] = new SelectList(BaseOptions.GetOptions<SystemFunctions>());
            return View(rolePermision);
        }

        // GET: RolePermisions/Edit/5
        public async Task<IActionResult> Edit(string rolecode, string functionrole)
        {
            if (string.IsNullOrWhiteSpace(rolecode) || string.IsNullOrWhiteSpace(functionrole))
            {
                return NotFound();
            }

            var rolePermision = await _context.RolePermision
                .FirstOrDefaultAsync(m => m.RoleCode == rolecode && m.FunctionRole == functionrole);
            if (rolePermision == null)
            {
                return NotFound();
            }
            return View(rolePermision);
        }

        // POST: RolePermisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string rolecode, string functionrole, [Bind("RoleCode,FunctionRole")] RolePermision rolePermision)
        {
            if (string.IsNullOrWhiteSpace(rolecode) || string.IsNullOrWhiteSpace(functionrole))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolePermision);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolePermisionExists(rolePermision.RoleCode))
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
            return View(rolePermision);
        }

        // GET: RolePermisions/Delete/5
        public async Task<IActionResult> Delete(string rolecode, string functionrole)
        {
            if (string.IsNullOrWhiteSpace(rolecode) || string.IsNullOrWhiteSpace(functionrole))
            {
                return NotFound();
            }

            var rolePermision = await _context.RolePermision
                .FirstOrDefaultAsync(m => m.RoleCode == rolecode && m.FunctionRole == functionrole);
            if (rolePermision == null)
            {
                return NotFound();
            }

            return View(rolePermision);
        }

        // POST: RolePermisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string rolecode, string functionrole)
        {
            if (string.IsNullOrWhiteSpace(rolecode) || string.IsNullOrWhiteSpace(functionrole))
            {
                return NotFound();
            }

            var rolePermision = await _context.RolePermision
                .FirstOrDefaultAsync(m => m.RoleCode == rolecode && m.FunctionRole == functionrole);
            if (rolePermision == null)
            {
                return NotFound();
            }

            _context.RolePermision.Remove(rolePermision);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolePermisionExists(string id)
        {
            return _context.RolePermision.Any(e => e.RoleCode == id);
        }
    }
}
