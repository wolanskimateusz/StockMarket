﻿using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
