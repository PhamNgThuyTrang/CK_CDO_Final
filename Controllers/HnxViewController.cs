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
    public class HnxViewController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public HnxViewController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        // GET: Hnxes
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, DateTime? date, int page = 1)
        {
            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["Search"] = searchString;
            }
            if (date != null)
            {
                ViewData["Date"] = Convert.ToDateTime(date).Date;
            }

            /*var hnx = from h in _context.Hnx
                      select h;

            if (!String.IsNullOrEmpty(searchString))
            {
                hnx = hnx.Where(a => a.MA.Contains(searchString.ToUpper()));

            }

            if (date != null)
            {
                hnx = hnx.Where(a => a.NGAY.Equals(date));

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
            */

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters() ;
            dynamicParameters.Add(name: ":h_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_Ngay", direction: ParameterDirection.Input, value: date, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":h_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var hnxs = db.Query<Hnx>("SP_PAGINATION_HNX", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewData["page"] = page;
            ViewData["total"] = hnxs.Count();

            return View(hnxs);
        }
    }
}