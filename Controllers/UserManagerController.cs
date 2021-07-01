using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CK_CDO_Final.Entities;
using Microsoft.AspNetCore.Identity;
using CK_CDO_Final.Models;
using Microsoft.AspNetCore.Authorization;
using PagedList.Core;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";


        public UserManagerController(OracleDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _config = config;

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int page = 1)
        {
            ViewData["Name"] = sortOrder == "Username" ? "UserName_desc" : "UserName";
            ViewData["Level"] = sortOrder == "Level" ? "Level_desc" : "Level";
      
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":u_Username", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":u_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":u_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var users = db.Query<ApplicationUser>("SP_PAGINATION_USER", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            var totalPage = _userManager.Users.Where(u => searchString == null || u.UserName.Contains(searchString.Trim().ToUpper())).Count() / 10;
            ViewData["total"] = _userManager.Users.Where(u => searchString == null || u.UserName.Contains(searchString.Trim().ToUpper())).Count() % 10
                            == 0 ? totalPage : totalPage + 1;

            return View(users);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            return View(user);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            return View(user);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber,FirstName,LastName,CreditCard,Address,DateOfBirth,Role")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (ModelState.IsValid)
            {
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.Address = applicationUser.Address;
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.DOB = applicationUser.DOB;
                user.Email = applicationUser.Email;
                user.UserName = applicationUser.UserName;
                user.Role = applicationUser.Role;

                try
                {
                    await _userManager.UpdateAsync(user);
                    if (applicationUser.Role == 0)
                    {
                        if (await _userManager.IsInRoleAsync(user, "Admin"))
                            await _userManager.RemoveFromRoleAsync(user, "admin");
                    }
                    else
                    {
                        if (!await _userManager.IsInRoleAsync(user, "Admin"))
                            await _userManager.AddToRoleAsync(user, "Admin");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(applicationUser.Id))
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
            return View(applicationUser);
        }

        private bool AccountExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }

    }

}
