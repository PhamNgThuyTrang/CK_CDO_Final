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
        public async Task<IActionResult> Index(int page = 1)
        {
            var companyDetails = _context.CompanyDetails;
            //Thực hiện phân trang với page là trang hiện tại, PAGE_SIZE số hàng hóa mỗi trang
            PagedList<CompanyDetails> model = new PagedList<CompanyDetails>(companyDetails, page, 10);
            return View(model);
        }
    }
}
