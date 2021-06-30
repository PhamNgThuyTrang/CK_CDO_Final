using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CK_CDO_Final.Entities;
using PagedList.Core;
using Microsoft.AspNetCore.Authorization;
using Dapper.Oracle;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyDetailsController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public CompanyDetailsController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: CompanyDetails
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1)
        {
            
            ViewData["Ma"] = sortOrder == "Ma" ? "Ma_desc" : "Ma";
            ViewData["Ten"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Nganh"] = sortOrder == "Nganh" ? "Nganh_desc" : "Nganh";
            ViewData["San"] = sortOrder == "San" ? "San_desc" : "San";
            ViewData["KLNY"] = sortOrder == "KLNY" ? "KLNY_desc" : "KLNY";



            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":c_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":c_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":c_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var companyDetails = db.Query<CompanyDetails>("SP_PAGINATION_COMPANY", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            var totalPage = _context.CompanyDetails.Where(c => searchString == null || c.MA.Contains(searchString.Trim().ToUpper())).Count() / 10;
            ViewData["total"] = _context.CompanyDetails.Where(c => searchString == null || c.MA.Contains(searchString.Trim().ToUpper())).Count() % 10
                            == 0 ? totalPage : totalPage + 1;

            return View(companyDetails);
        }

        // GET: CompanyDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDetails = await _context.CompanyDetails
                .FirstOrDefaultAsync(m => m.MA == id);
            if (companyDetails == null)
            {
                return NotFound();
            }

            return View(companyDetails);
        }

        // GET: CompanyDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MA,TEN,NGANHNGHE,SAN,KLNY")] CompanyDetails companyDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyDetails);
        }

        // GET: CompanyDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDetails = await _context.CompanyDetails.FindAsync(id);
            if (companyDetails == null)
            {
                return NotFound();
            }
            return View(companyDetails);
        }

        // POST: CompanyDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MA,TEN,NGANHNGHE,SAN,KLNY")] CompanyDetails companyDetails)
        {
            if (id != companyDetails.MA)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyDetailsExists(companyDetails.MA))
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
            return View(companyDetails);
        }

        // GET: CompanyDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDetails = await _context.CompanyDetails
                .FirstOrDefaultAsync(m => m.MA == id);
            if (companyDetails == null)
            {
                return NotFound();
            }

            return View(companyDetails);
        }

        // POST: CompanyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var companyDetails = await _context.CompanyDetails.FindAsync(id);
            _context.CompanyDetails.Remove(companyDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyDetailsExists(string id)
        {
            return _context.CompanyDetails.Any(e => e.MA == id);
        }
    }
}
