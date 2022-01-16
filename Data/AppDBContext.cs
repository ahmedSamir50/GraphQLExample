using GraphQLOne.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace GraphQLOne.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions opt) : base(opt)
        {
            //this.Database.EnsureCreated();
            //Console.WriteLine("****" +this.Database.GetConnectionString()+ "****");
        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modBuild)
        {
            modBuild.Entity<Platform>()
                    .HasMany(x => x.Commands)
                    .WithOne(x => x.Platform)
                    .HasForeignKey(f => f.PlateformID);

            modBuild.Entity<Command>()
                .HasOne(c => c.Platform)
                .WithMany(p => p.Commands)
                .HasForeignKey(fk => fk.PlateformID);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite( options =>
        //    {
        //        options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        //    });
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
