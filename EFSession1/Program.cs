using EFSession1.Context;
using EFSession1.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace EFSession1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // CRUD operations
            using BookStoreContext dbContext = new BookStoreContext(); // => "using" for dispose dbContext when finish work with it

            # region Create
            
            Book b1 = new Book() { Title = "C# Book", Description = "C# Book Description", Price = 100 }; // his state is Detached
            Book b2 = new Book() { Title = "Java Book", Description = "Java Book Description", Price = 200 };

            //Add Local
            dbContext.Books.Add(b1); // it add local => not add in database => it add to database when i call SaveChanges() => his state is Added
            dbContext.Books.Add(b2); // if i run the program again => it add again => i have 2 books with same data

            //Add Remote
            dbContext.SaveChanges(); // it add in database => state of files is Unchanged


            Console.WriteLine(dbContext.Entry(b2).State);
            // 0 - Detached => not exist in database and not tracked by dbContext
            // 1 - Unchanged => exist in database and tracked by dbContext and dont happend on it any changes
            // 3 - Deleted => not exist in database and tracked by dbContext and happend on it delete operation
            // 4 - Modified => exist in database and tracked by dbContext and happend on it update operation
            // 2 - Added =>  exist in database and tracked by dbContext and happend on it add operation

            #endregion

            #region Retrieve
            var RetrievedBook= dbContext.Books.FirstOrDefault(); // it return book with id=1 => his state is Unchanged


            var RetrievedBook2 = dbContext.Books.Include(b=>b.Author).SingleOrDefault(b=>b.id==1); // when select the book from the data base it select the author with it => Eager Loading helps you to load all your needed entities at once

            var RetrievedBook3 = dbContext.Books.SingleOrDefault(b => b.id == 1); 
            dbContext.Entry(RetrievedBook3).Reference(b => b.Author).Load(); // => this calling Explicit Loading mean load the entities by explicitly calling the Load method for the related entities.
                //When to use what
                //Use Eager Loading when the relations are not too much. Thus, Eager Loading is a good practice to reduce further queries on the Server.
                //Use Eager Loading when you are sure that you will be using related entities with the main entity everywhere.
                //Use Lazy Loading when you are using one-to - many collections.
                //Use Lazy Loading when you are sure that you are not using related entities instantly.
                //When you have turned off Lazy Loading, use Explicit loading when you are not sure whether or not you will be using an entity beforehand.


            #endregion


            #region Update

            var Book1 = dbContext.Books.Find(10);
            Book1.Title = "C# Book Updated";
            dbContext.SaveChanges(); // it update in database 
            #endregion

            #region Delete
            var Book = dbContext.Books.LastOrDefault();
            dbContext.Remove(Book); // it delete from dbContext => his state is Deleted
            dbContext.SaveChanges(); // it delete from database
            #endregion

            // i dont use explicit loading 
            // i use eager loading with the side of one relation becouse it it fetch small amount of data then it fast
            // i use lazy loading with the side of many relation becouse it it fetch large amount of data then we use lazy loading to fetch data when we need it


        }
    }
}