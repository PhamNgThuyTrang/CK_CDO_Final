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
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, DateTime? date, int page = 1)
        {
            ViewData["Ma"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["Close"] = sortOrder == "Close" ? "Close_desc" : "Close";
            ViewData["Open"] = sortOrder == "Open" ? "Open_desc" : "Open";

            /*var hoses = from h in _context.Hose
                        select h;
            
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
            return View(model);*/

            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            OracleDynamicParameters dynamicParameters = new OracleDynamicParameters();
            dynamicParameters.Add(name: ":h_Ma", direction: ParameterDirection.Input, value: searchString, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_Ngay", direction: ParameterDirection.Input, value: date, dbType: OracleMappingType.Date);
            dynamicParameters.Add(name: ":h_sortOrder", direction: ParameterDirection.Input, value: sortOrder, dbType: OracleMappingType.Varchar2);
            dynamicParameters.Add(name: ":h_pageIndex", direction: ParameterDirection.Input, value: page, dbType: OracleMappingType.Int32);
            dynamicParameters.Add(name: ":cv_1", direction: ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

            var hoses = db.Query<Hose>("SP_PAGINATION_HOSE", param: dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            return View(hoses);
        }
    }
}
