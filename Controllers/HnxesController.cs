﻿using System;
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
using Microsoft.Extensions.Configuration;
using Dapper;

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HnxesController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public HnxesController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        // GET: Hnxes
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, DateTime? date, int page = 1)
        {

            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";

            ViewData["page"] = page;
            ViewData["total"] = _context.Hnx.Count();

            /*var hnx = from h in _context.Hnx
                         select h;

            if (!String.IsNullOrEmpty(searchString))
            {
                hnx = hnx.Where(a => a.MA.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            if (date != null)
            {
                hnx = hnx.Where(a => a.NGAY.Equals(date));
                ViewData["Date"] = date;

            }

            switch (sortOrder)
            {

                case "Name":
                    hnx = hnx.OrderBy(a => a.MA);
                    break;
                case "Name_desc":
                    hnx = hnx.OrderByDescending(a => a.MA);
                    break;
                case "Close":
                    hnx = hnx.OrderBy(a => a.GIAMOCUA);
                    break;
                case "Close_desc":
                    hnx = hnx.OrderByDescending(a => a.GIAMOCUA);
                    break;
                case "Open":
                    hnx = hnx.OrderBy(a => a.GIADONGCUA);
                    break;
                case "Open_desc":
                    hnx = hnx.OrderByDescending(a => a.GIADONGCUA);
                    break;
                default:
                    hnx = hnx.OrderByDescending(a => a.ID);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Hnx> model = new PagedList<Hnx>(hnx, page, 10);
            return View(model);
            */

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":h_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_Ngay", direction: ParameterDirection.Input, value: date, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":h_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var hnxs = db.Query<Hnx>("SP_PAGINATION_HNX", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            return View(hnxs);
        }

        // GET: Hnxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hnx = await _context.Hnx
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hnx == null)
            {
                return NotFound();
            }

            return View(hnx);
        }

        // GET: Hnxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hnxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MA,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Hnx hnx)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hnx);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hnx);
        }

        // GET: Hnxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hnx = await _context.Hnx.FindAsync(id);
            if (hnx == null)
            {
                return NotFound();
            }
            return View(hnx);
        }

        // POST: Hnxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MA,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Hnx hnx)
        {
            if (id != hnx.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hnx);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HnxExists(hnx.ID))
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
            return View(hnx);
        }

        // GET: Hnxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hnx = await _context.Hnx
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hnx == null)
            {
                return NotFound();
            }

            return View(hnx);
        }

        // POST: Hnxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hnx = await _context.Hnx.FindAsync(id);
            _context.Hnx.Remove(hnx);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HnxExists(int id)
        {
            return _context.Hnx.Any(e => e.ID == id);
        }
    }
}
