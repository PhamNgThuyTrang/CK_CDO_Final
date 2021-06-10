using CK_CDO_Final.Entities;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Controllers
{
    public class CompanyDetailsView : Controller
    {
        private readonly OracleDbContext _context;

        public CompanyDetailsView(OracleDbContext context)
        {
            _context = context;
        }

        // GET: CompanyDetails
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1)
        {
            var companyDetails = from c in _context.CompanyDetails
                                    select c;

            ViewData["Ma"] = sortOrder == "Ma" ? "Ma_desc" : "Ma";
            ViewData["Ten"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Nganh"] = sortOrder == "Nganh" ? "Nganh_desc" : "Nganh";
            ViewData["San"] = sortOrder == "San" ? "San_desc" : "San";


            if (!String.IsNullOrEmpty(searchString))
            {
                companyDetails = companyDetails.Where(a => a.MA.Contains(searchString.ToUpper()));
                ViewData["Search"] = searchString;

            }

            switch (sortOrder)
            {

                case "Name":
                    companyDetails = companyDetails.OrderBy(a => a.TEN);
                    break;
                case "Name_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.TEN);
                    break;
                case "Nganh":
                    companyDetails = companyDetails.OrderBy(a => a.NGANHNGHE);
                    break;
                case "Nganh_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.NGANHNGHE);
                    break;
                case "San":
                    companyDetails = companyDetails.OrderBy(a => a.SAN);
                    break;
                case "San_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.SAN);
                    break;
                case "Ma_desc":
                    companyDetails = companyDetails.OrderByDescending(a => a.MA);
                    break;
                default:
                    companyDetails = companyDetails.OrderBy(a => a.MA);
                    break;
            }

            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<CompanyDetails> model = new PagedList<CompanyDetails>(companyDetails, page, 10);
            return View(model);
        }
    }
}
