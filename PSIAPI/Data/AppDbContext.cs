﻿using Microsoft.EntityFrameworkCore;
using PSIAPI.Models;
namespace PSIAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<LocationItem> LocationItems => Set<LocationItem>();
        public DbSet<LogItem> LogItems => Set<LogItem>();
        public DbSet<ReportItem> ReportItems => Set<ReportItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        
    }
}
