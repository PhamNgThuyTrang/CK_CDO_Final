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
    public class HosesController : Controller
    {
        private readonly OracleDbContext _context;

        public HosesController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Hoses
        public async Task<IActionResult> Index(int page = 1)
        {
            var hose = _context.Hose;
            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList <Hose> model = new PagedList<Hose>(hose, page, 10);
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
