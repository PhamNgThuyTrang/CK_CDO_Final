using CK_CDO_Final.Entities;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Controllers
{
    public class UpcomView : Controller
    {
        private readonly OracleDbContext _context;

        public UpcomView(OracleDbContext context)
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
    }
}
