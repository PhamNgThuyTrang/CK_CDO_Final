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

namespace CK_CDO_Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IndicesController : Controller
    {
        private readonly OracleDbContext _context;

        public IndicesController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Indices
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, DateTime? date, int page = 1)
        {
            var index = from i in _context.Index
                         select i;

            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";


            if (!String.IsNullOrEmpty(searchString))
            {
                index = index.Where(a => a.CHISO.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            if (date != null)
            {
                index = index.Where(a => a.NGAY.Equals(date));
                ViewData["Date"] = date;

            }

            switch (sortOrder)
            {

                case "Name":
                    index = index.OrderBy(a => a.CHISO);
                    break;
                case "Name_desc":
                    index = index.OrderByDescending(a => a.CHISO);
                    break;
                case "Close":
                    index = index.OrderBy(a => a.MOCUA);
                    break;
                case "Close_desc":
                    index = index.OrderByDescending(a => a.MOCUA);
                    break;
                case "Open":
                    index = index.OrderBy(a => a.DONGCUA);
                    break;
                case "Open_desc":
                    index = index.OrderByDescending(a => a.DONGCUA);
                    break;
                default:
                    index = index.OrderByDescending(a => a.ID);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Index> model = new PagedList<Index>(index, page, 10);
            return View(model);
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
