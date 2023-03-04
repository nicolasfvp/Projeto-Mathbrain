using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RankingApi.Models;

namespace RankingApi
{
    public class RankingApiContext : DbContext
    {
        public RankingApiContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmProcesso> EmProcesso { get; set;}
       
    }
}