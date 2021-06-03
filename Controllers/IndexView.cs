using CK_CDO_Final.Entities;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Index = CK_CDO_Final.Entities.Index;

namespace CK_CDO_Final.Controllers
{
    public class IndexView : Controller
    {
        private readonly OracleDbContext _context;

        public IndexView(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Hnxes
        public async Task<IActionResult> Index(int page = 1)
        {
            var index = _context.Index;
            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Index> model = new PagedList<Index>(index, page, 10);
            return View(model);
        }
    }
}
