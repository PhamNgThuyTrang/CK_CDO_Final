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

namespace CK_CDO_Final.Controllers
{
    public class IndicesController : Controller
    {
        private readonly OracleDbContext _context;

        public IndicesController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Indices
        public async Task<IActionResult> Index(int page = 1)
        {
            var index = _context.Index;
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
