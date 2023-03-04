using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbWrite.Models;
using Microsoft.EntityFrameworkCore;

namespace DbWrite
{
    public class DbWriteContext : DbContext
    {
        public DbSet<UserRanking> UserRanking { get; set; }
        public DbSet<EmProcesso> EmProcesso { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MathBrainRankingDb;Trusted_Connection=True;persist security info=True;");
        }
    }
}
