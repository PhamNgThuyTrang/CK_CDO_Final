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
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UpcomsController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public UpcomsController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        // GET: Upcoms
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, string? date, int page = 1)
        {
            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";
            ViewData["Ngay"] = sortOrder == "Ngay" ? "Ngay_desc" : "Ngay";


            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }

            DateTime? dateTime = null;

            if (date != null)
            {
                ViewData["Date"] = date;
                dateTime = Convert.ToDateTime(date);
            }

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":u_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":u_Ngay", direction: ParameterDirection.Input, value: dateTime, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":u_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":u_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var upcoms = db.Query<Upcom>("SP_PAGINATION_UPCOM", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            var totalPage = _context.Upcom.Where(c => (searchString == null || c.MA.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() / 10;
            ViewData["total"] = _context.Upcom.Where(c => (searchString == null || c.MA.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() % 10
                            == 0 ? totalPage : totalPage + 1;

            return View(upcoms);
        }

        public async Task<IActionResult> CompanyView(string id, string? searchString, string? date)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }
            if (date != null)
            {
                ViewData["Date"] = date;
            }
            var companyDetails = await _context.CompanyDetails
                .FirstOrDefaultAsync(m => m.MA == id);
            if (companyDetails == null)
            {
                return NotFound();
            }

            return View(companyDetails);
        }

        // GET: Upcoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upcom = await _context.Upcom
                .FirstOrDefaultAsync(m => m.ID == id);
            if (upcom == null)
            {
                return NotFound();
            }

            return View(upcom);
        }

        // GET: Upcoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Upcoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MA,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Upcom upcom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(upcom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(upcom);
        }

        // GET: Upcoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upcom = await _context.Upcom.FindAsync(id);
            if (upcom == null)
            {
                return NotFound();
            }
            return View(upcom);
        }

        // POST: Upcoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MA,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Upcom upcom)
        {
            if (id != upcom.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upcom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpcomExists(upcom.ID))
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
            return View(upcom);
        }

        // GET: Upcoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upcom = await _context.Upcom
                .FirstOrDefaultAsync(m => m.ID == id);
            if (upcom == null)
            {
                return NotFound();
            }

            return View(upcom);
        }

        // POST: Upcoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upcom = await _context.Upcom.FindAsync(id);
            _context.Upcom.Remove(upcom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpcomExists(int id)
        {
            return _context.Upcom.Any(e => e.ID == id);
        }
    }
}
