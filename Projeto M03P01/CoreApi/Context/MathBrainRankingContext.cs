using CoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoreApi.Context
{
    public class MathBrainRankingContext : DbContext
    {
        public MathBrainRankingContext()
        {

        }
        public MathBrainRankingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserRanking> UserRanking { get; set; }

        public DbSet<EmProcesso> EmProcesso { get; set; }
    }
}
