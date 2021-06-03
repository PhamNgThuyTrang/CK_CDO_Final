using CK_CDO_Final.Entities;
using CK_CDO_Final.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Entities
{
    public class OracleDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Hose> Hose { get; set; }
        public DbSet<Hnx> Hnx { get; set; }
        public DbSet<Upcom> Upcom { get; set; }
        public DbSet<Index> Index { get; set; }
        public DbSet<CompanyDetails> CompanyDetails { get; set; }

        public OracleDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
