using CK_CDO_Final.Entities;
using Dapper;
using Dapper.Oracle;
using Microsoft.AspNetCore.Mvc;
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
    public class CompanyDetailsViewController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public CompanyDetailsViewController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: CompanyDetails
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1)
        {
            ViewData["Ma"] = sortOrder == "Ma" ? "Ma_desc" : "Ma";
            ViewData["Ten"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Nganh"] = sortOrder == "Nganh" ? "Nganh_desc" : "Nganh";
            ViewData["San"] = sortOrder == "San" ? "San_desc" : "San";
            ViewData["KLNY"] = sortOrder == "KLNY" ? "KLNY_desc" : "KLNY";

            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":c_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":c_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":c_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var companyDetails = db.Query<CompanyDetails>("SP_PAGINATION_COMPANY", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            var totalPage = _context.CompanyDetails.Where(c => searchString == null || c.MA.Contains(searchString.Trim().ToUpper())).Count() / 10;
            ViewData["total"] = _context.CompanyDetails.Where(c => searchString == null || c.MA.Contains(searchString.Trim().ToUpper())).Count() % 10
                            == 0 ? totalPage : totalPage + 1;

            return View(companyDetails);
        }
    }
}
