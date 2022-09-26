﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramwork
{
    public partial class PersonalContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyData")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        }

        //public  DbSet<Card> Cards { get; set; }
        //public  DbSet<Adress> Adresses { get; set; }
        //public  DbSet<Orders> Orders { get; set; }
        //public  DbSet<Product> Products { get; set; }
        //public  DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.HasDefaultSchema("dbo");

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
