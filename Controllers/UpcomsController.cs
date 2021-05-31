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
    public class UpcomsController : Controller
    {
        private readonly OracleDbContext _context;

        public UpcomsController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Upcoms
        public async Task<IActionResult> Index(int page = 1)
        {
            var upcoms = _context.Upcom;
            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Upcom> model = new PagedList<Upcom>(upcoms, page, 10);
            return View(model);
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
