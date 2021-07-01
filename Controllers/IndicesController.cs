using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CK_CDO_Final.Entities;
using PagedList.Core;
using Index = CK_CDO_Final.Entities.Index;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper.Oracle;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IndicesController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public IndicesController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Indices
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
            dynamicParameters.Add(name: ":i_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":i_Ngay", direction: ParameterDirection.Input, value: dateTime, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":i_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":i_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var indices = db.Query<Index>("SP_PAGINATION_INDEX", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            var totalPage = _context.Index.Where(c => (searchString == null || c.CHISO.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() / 10;
            ViewData["total"] = _context.Index.Where(c => (searchString == null || c.CHISO.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() % 10
                            == 0 ? totalPage : totalPage + 1;

            return View(indices);
        }
    

        // GET: Indices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var index = await _context.Index
                .FirstOrDefaultAsync(m => m.ID == id);
            if (index == null)
            {
                return NotFound();
            }

            return View(index);
        }

        // GET: Indices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Indices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CHISO,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Entities.Index index)
        {
            if (ModelState.IsValid)
            {
                _context.Add(index);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(index);
        }

        // GET: Indices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var index = await _context.Index.FindAsync(id);
            if (index == null)
            {
                return NotFound();
            }
            return View(index);
        }

        // POST: Indices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CHISO,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Entities.Index index)
        {
            if (id != index.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(index);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndexExists(index.ID))
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
            return View(index);
        }

        // GET: Indices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var index = await _context.Index
                .FirstOrDefaultAsync(m => m.ID == id);
            if (index == null)
            {
                return NotFound();
            }

            return View(index);
        }

        // POST: Indices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var index = await _context.Index.FindAsync(id);
            _context.Index.Remove(index);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndexExists(int id)
        {
            return _context.Index.Any(e => e.ID == id);
        }
    }
}
