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

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HosesController : Controller
    {
        private readonly OracleDbContext _context;

        public HosesController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Hoses
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, DateTime? date, int page = 1)
        {
            var hoses = from h in _context.Hose
                        select h;

            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";


            if (!String.IsNullOrEmpty(searchString))
            {
                hoses = hoses.Where(a => a.MA.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            if (date != null)
            {
                hoses = hoses.Where(a => a.NGAY.Equals(date));
                ViewData["Date"] = date;

            }

            switch (sortOrder)
            {

                case "Name":
                    hoses = hoses.OrderBy(a => a.MA);
                    break;
                case "Name_desc":
                    hoses = hoses.OrderByDescending(a => a.MA);
                    break;
                case "Close":
                    hoses = hoses.OrderBy(a => a.GIAMOCUA);
                    break;
                case "Close_desc":
                    hoses = hoses.OrderByDescending(a => a.GIAMOCUA);
                    break;
                case "Open":
                    hoses = hoses.OrderBy(a => a.GIADONGCUA);
                    break;
                case "Open_desc":
                    hoses = hoses.OrderByDescending(a => a.GIADONGCUA);
                    break;
                default:
                    hoses = hoses.OrderByDescending(a => a.ID);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Hose> model = new PagedList<Hose>(hoses, page, 10);
            return View(model);

        }

        // GET: Hoses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hose = await _context.Hose
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hose == null)
            {
                return NotFound();
            }

            return View(hose);
        }

        // GET: Hoses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hoses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MA,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Hose hose)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hose);
        }

        // GET: Hoses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hose = await _context.Hose.FindAsync(id);
            if (hose == null)
            {
                return NotFound();
            }
            return View(hose);
        }

        // POST: Hoses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MA,NGAY,GIAMOCUA,GIATRAN,GIASAN,GIADONGCUA,KHOILUONG")] Hose hose)
        {
            if (id != hose.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoseExists(hose.ID))
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
            return View(hose);
        }

        // GET: Hoses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hose = await _context.Hose
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hose == null)
            {
                return NotFound();
            }

            return View(hose);
        }

        // POST: Hoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hose = await _context.Hose.FindAsync(id);
            _context.Hose.Remove(hose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoseExists(int id)
        {
            return _context.Hose.Any(e => e.ID == id);
        }
    }
}
