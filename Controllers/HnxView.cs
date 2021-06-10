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
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, DateTime? date, int page = 1)
        {
            var hnx = from h in _context.Hnx
                      select h;

            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";


            if (!String.IsNullOrEmpty(searchString))
            {
                hnx = hnx.Where(a => a.MA.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            if (date != null)
            {
                hnx = hnx.Where(a => a.NGAY.Equals(date));
                ViewData["Date"] = date;

            }

            switch (sortOrder)
            {

                case "Name":
                    hnx = hnx.OrderBy(a => a.MA);
                    break;
                case "Name_desc":
                    hnx = hnx.OrderByDescending(a => a.MA);
                    break;
                case "Close":
                    hnx = hnx.OrderBy(a => a.GIAMOCUA);
                    break;
                case "Close_desc":
                    hnx = hnx.OrderByDescending(a => a.GIAMOCUA);
                    break;
                case "Open":
                    hnx = hnx.OrderBy(a => a.GIADONGCUA);
                    break;
                case "Open_desc":
                    hnx = hnx.OrderByDescending(a => a.GIADONGCUA);
                    break;
                default:
                    hnx = hnx.OrderByDescending(a => a.ID);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Hnx> model = new PagedList<Hnx>(hnx, page, 10);
            return View(model);
        }
    }
}
