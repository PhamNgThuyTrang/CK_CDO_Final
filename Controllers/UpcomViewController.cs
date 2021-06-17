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
    public class UpcomViewController : Controller
    {
        private readonly OracleDbContext _context;
        private readonly IConfiguration _config;
        private string Connectionstring = "CK[CDO]";

        public UpcomViewController(OracleDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        // GET: Upcoms
        public async Task<IActionResult> Index(string ?searchString, string ?sortOrder, DateTime ?date, int page = 1)
        {
            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";

            /*var upcoms = from u in _context.Upcom
                         select u ;
            
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
            */

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":u_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":u_Ngay", direction: ParameterDirection.Input, value: date, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":u_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":u_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var upcoms = db.Query<Upcom>("SP_PAGINATION_UPCOM", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            return View(upcoms);
        }
    }
}
