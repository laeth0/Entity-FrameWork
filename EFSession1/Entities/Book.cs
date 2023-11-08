using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSession1.Entities
{//Mapping 3 Ways
 //Mapping
 // 1) By Convention
 // 2) By Data Annotation
 // 3) By Fluent API => use when i need to change default behavior and i dont have access to source code and for use it i need to override methods in DbContext class


    [Table("Book_Library")] // =>to change Table Name in Database
    public class Book
    {   
        // Mapping By Data Annotation
        [Key] // => PrimaryKey
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// => Identity(1,1)
        public int BookID { get; set;} //int named like Id => PrimaryKey Identity(1,1)

        [Required] // => Not Null
        [Column(TypeName = "varchar")] // => nvarchar
        //[MaxLength(100)] // => nvarchar(100)
        [StringLength(100,MinimumLength =10)] // => string between 10 and 100
        public string Title { get; set;}

        [Required]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]// affect in UI only => not affect in database
        [Column(TypeName = "money")] // affect in database only 
        [Range(0,20)]// it range for number from 0 to 20 and affect in UI only  
        public double Price { get; set; }


        [NotMapped] // => use to Exclude this property from database
        public int? AuthorID { get; set; }


        [Column("DateOfBorn")]//=> change column name in database
        [Comment("this column is changed his name")]
        public string DateOfCreate { get; set; }

        public string Fname { get; set; }
        public string Lname { get; set; }
        public string FullName { get; set; }


        public override string ToString()
        {
            return "BookID: " + BookID + " Title: " + Title + " Description: " + Description + " Price: " + Price + "\n";
        }

    }
}
