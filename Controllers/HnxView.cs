using CK_CDO_Final.Entities;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Controllers
{
    public class HnxView : Controller
    {
        private readonly OracleDbContext _context;

        public HnxView(OracleDbContext context)
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
    }
}
