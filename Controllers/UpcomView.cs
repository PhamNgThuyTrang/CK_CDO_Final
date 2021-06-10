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
        public async Task<IActionResult> Index(string ?searchString, string ?sortOrder, DateTime ?date, int page = 1)
        {
            var upcoms = from u in _context.Upcom
                         select u ;

            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";


            if (!String.IsNullOrEmpty(searchString))
            {
                upcoms = upcoms.Where(a => a.MA.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            if (date != null)
            {
                upcoms = upcoms.Where(a => a.NGAY.Equals(date));
                ViewData["Date"] = date;

            }

            switch (sortOrder)
            {

                case "Name":
                    upcoms = upcoms.OrderBy(a => a.MA);
                    break;
                case "Name_desc":
                    upcoms = upcoms.OrderByDescending(a => a.MA);
                    break;
                case "Close":
                    upcoms = upcoms.OrderBy(a => a.GIAMOCUA);
                    break;
                case "Close_desc":
                    upcoms = upcoms.OrderByDescending(a => a.GIAMOCUA);
                    break;
                case "Open":
                    upcoms = upcoms.OrderBy(a => a.GIADONGCUA);
                    break;
                case "Open_desc":
                    upcoms = upcoms.OrderByDescending(a => a.GIADONGCUA);
                    break;
                default:
                    upcoms = upcoms.OrderByDescending(a => a.ID);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<Upcom> model = new PagedList<Upcom>(upcoms, page, 10);
            return View(model);
        }
    }
}
