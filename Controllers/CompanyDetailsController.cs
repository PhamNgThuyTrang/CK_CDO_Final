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

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompanyDetailsController : Controller
    {
        private readonly OracleDbContext _context;

        public CompanyDetailsController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: CompanyDetails
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1)
        {
            var companyDetails = from c in _context.CompanyDetails
                                 select c;

            ViewData["Ma"] = sortOrder == "Ma" ? "Ma_desc" : "Ma";
            ViewData["Ten"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Nganh"] = sortOrder == "Nganh" ? "Nganh_desc" : "Nganh";
            ViewData["San"] = sortOrder == "San" ? "San_desc" : "San";


            if (!String.IsNullOrEmpty(searchString))
            {
                companyDetails = companyDetails.Where(a => a.MA.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            switch (sortOrder)
            {

                case "Name":
                    companyDetails = companyDetails.OrderBy(a => a.TEN);
                    break;
                case "Name_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.TEN);
                    break;
                case "Nganh":
                    companyDetails = companyDetails.OrderBy(a => a.NGANHNGHE);
                    break;
                case "Nganh_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.NGANHNGHE);
                    break;
                case "San":
                    companyDetails = companyDetails.OrderBy(a => a.SAN);
                    break;
                case "San_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.SAN);
                    break;
                case "Ma_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.MA);
                    break;
                default:
                    companyDetails = companyDetails.OrderBy(a => a.MA);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<CompanyDetails> model = new PagedList<CompanyDetails>(companyDetails, page, 10);
            return View(model);
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