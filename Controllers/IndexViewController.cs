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
    public class IndexViewController : Controller
    {
        private readonly OracleDbContext _context;

        public IndexViewController(OracleDbContext context)
        {
            _context = context;
        }

        // GET: Hnxes
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
    }
}
