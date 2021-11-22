using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ABSA.PB.Models;

namespace ABSA.PB.Data
{
    public class PhonebookDBContext : DbContext
    {
        public PhonebookDBContext(DbContextOptions<PhonebookDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
        }

        public DbSet<Phonebook> Phonebooks { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }
}