using CK_CDO_Final.Entities;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Controllers
{
    public class HoseView : Controller
    {
        private readonly OracleDbContext _context;

        public HoseView(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Hoses
        public async Task<IActionResult> Index(int page = 1)
        {
            var hose = _context.Hose;
            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Hose> model = new PagedList<Hose>(hose, page, 10);
            return View(model);

        }
    }
}
