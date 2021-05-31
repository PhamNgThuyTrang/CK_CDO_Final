using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CK_CDO_Final.Entities;
using PagedList.Core;

namespace CK_CDO_Final.Controllers
{
    public class HnxesController : Controller
    {
        private readonly OracleDbContext _context;

        public HnxesController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Hnxes
        public async Task<IActionResult> Index(int page = 1)
        {
            var hnx = _context.Hnx;
            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Hnx> model = new PagedList<Hnx>(hnx, page, 10);
            return View(model);
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
