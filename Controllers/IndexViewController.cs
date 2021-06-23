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
using Index = CK_CDO_Final.Entities.Index;

namespace CK_CDO_Final.Controllers
{
    public class IndexViewController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public IndexViewController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Hnxes
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
            dynamicParameters.Add(name: ":i_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":i_Ngay", direction: ParameterDirection.Input, value: dateTime, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":i_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":i_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var indices = db.Query<Index>("SP_PAGINATION_INDEX", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            var totalPage = _context.Index.Where(c => (searchString == null || c.CHISO.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() / 10;
            ViewData["total"] = _context.Index.Where(c => (searchString == null || c.CHISO.Contains(searchString.Trim().ToUpper())) && (dateTime == null || c.NGAY == dateTime)).Count() % 10
                            == 0 ? totalPage : totalPage + 1;

            return View(indices);
        }
    }
}
