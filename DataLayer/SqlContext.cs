using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace DataLayer
{
    public class SqlContext : DbContext
    {
        public virtual DbSet<document> documents { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<document_files> Document_Files { get; set; }
        public SqlContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DeflautConnetion");
            }
        }
    }
}
