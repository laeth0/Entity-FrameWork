using EFSession1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSession1.Context
{
    public class BookStoreContext:DbContext
    {
        // add-migration "InitialCreate"
        // update-database => it use up
        // Update-Database -migration:0 => revert all migration and update database => it use down
        // Update-Database -migration:InitialCreate => revert database to InitialCreate migration 
        // Remove-Migration => Remove last migration but i cant remove last migration if i update database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;DataBase=MVCDB;Trusted_Connection=true;MultipleActiveResultSets=True");
        }

        public DbSet<Book> Books { get; set; } //Collection of Books => Table of Books in Database 

        protected override void OnModelCreating(ModelBuilder modelBuilder)// => Mapping By Fluent API
        {
            modelBuilder.Entity<Book>().HasKey(B=>new { B.BookID,B.Title});// => Make BookID and Title Composite key by Fluent API
            modelBuilder.Entity<Book>().HasKey(b => b.BookID);// => Make BookID PrimaryKey by Fluent API
            modelBuilder.Entity<Book>().Property(b => b.BookID).UseIdentityColumn(10, 10); // => Identity(10,10) by Fluent API
            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired(false).HasDefaultValue("Not Found").HasColumnType("varchar").HasMaxLength(100);// => Title is not required and default value is "Not Found" and type is varchar and max length is 100 by Fluent API
            modelBuilder.Entity<Book>(E =>
            {// E refers to Book Entity 
                E.Property(b => b.Description).IsRequired(true).HasColumnType("varchar(max)");// => Description is required and type is nvarchar(max) by Fluent 
            });

            modelBuilder.Ignore<Book>();// => Exclude Book Entity by Fluent API => this use when i create a class and i dont need to create table for it in database
            modelBuilder.Entity<Book>().ToTable("Book_Library");// => Change Table Name in Database by Fluent API 
            modelBuilder.Entity<Book>().Ignore(B=>B.AuthorID);// => Exclude AuthorID Property from Book Entity by Fluent API => this use when i need to exclude property from database but i need to use it in my code
            modelBuilder.Entity<Book>().Property(b => b.DateOfCreate).HasColumnType("DateOfBorn");// => Change Column Name in Database by Fluent 

            modelBuilder.Entity<Book>().Property(B=>B.DateOfCreate).HasComment("this column is changed his name");// => Add Comment to Column in Database by Fluent API

            modelBuilder.Entity<Book>().Property(B=>B.FullName).HasComputedColumnSql("[Fname] + ' ' + [Lname]");// => Add Computed Column in Database by Fluent API => this use when i need to add column in database and i need to compute it from other columns in database

            modelBuilder.Entity<Book>().HasIndex(B=>B.BookID).IsUnique();// => Add Unique Index to Column in Database 
            modelBuilder.Entity<Book>().HasIndex(B => new { B.Title, B.Description });









            base.OnModelCreating(modelBuilder);// => for use default 
        }

    }
}
