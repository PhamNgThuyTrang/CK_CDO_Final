using CK_CDO_Final.Entities;
using Dapper;
using Dapper.Oracle;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Controllers
{
    public class HoseViewController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public HoseViewController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        // GET: Hoses
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, string? date, int page = 1)
        {
            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }

            DateTime? dateTime = null;

            if (date != null)
            {
                ViewData["Date"] = date;
                dateTime = Convert.ToDateTime(date);
            }
            
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":h_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_Ngay", direction: ParameterDirection.Input, value: dateTime, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":h_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var hoses = db.Query<Hose>("SP_PAGINATION_HOSE", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            var totalPage = _context.Hose.Where(c => (searchString == null || c.MA.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() / 10;
            ViewData["total"] = _context.Hose.Where(c => (searchString == null || c.MA.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() % 10
                            == 0 ? totalPage : totalPage + 1;

            return View(hoses);
        }
        public async Task<IActionResult> CompanyView(string id, string? searchString, string? date)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }
            if (date != null)
            {
                ViewData["Date"] = date;
            }
            var companyDetails = await _context.CompanyDetails
                .FirstOrDefaultAsync(m => m.MA == id);
            if (companyDetails == null)
            {
                return NotFound();
            }

            return View(companyDetails);
        }
    }
}
