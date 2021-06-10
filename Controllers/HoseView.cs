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
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, DateTime? date, int page = 1)
        {
            var hoses = from h in _context.Hose
                        select h;

            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";


            if (!String.IsNullOrEmpty(searchString))
            {
                hoses = hoses.Where(a => a.MA.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            if (date != null)
            {
                hoses = hoses.Where(a => a.NGAY.Equals(date));
                ViewData["Date"] = date;

            }

            switch (sortOrder)
            {

                case "Name":
                    hoses = hoses.OrderBy(a => a.MA);
                    break;
                case "Name_desc":
                    hoses = hoses.OrderByDescending(a => a.MA);
                    break;
                case "Close":
                    hoses = hoses.OrderBy(a => a.GIAMOCUA);
                    break;
                case "Close_desc":
                    hoses = hoses.OrderByDescending(a => a.GIAMOCUA);
                    break;
                case "Open":
                    hoses = hoses.OrderBy(a => a.GIADONGCUA);
                    break;
                case "Open_desc":
                    hoses = hoses.OrderByDescending(a => a.GIADONGCUA);
                    break;
                default:
                    hoses = hoses.OrderByDescending(a => a.ID);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Hose> model = new PagedList<Hose>(hoses, page, 10);
            return View(model);
        }
    }
}
