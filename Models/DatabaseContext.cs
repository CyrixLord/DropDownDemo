using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropDownDemo.Models
{
    public class DatabaseContext  : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)   // constructor
        {
        }

        // 
        // here is where we add methods representing the tables in the models folder so they can be included 
        // into the entity framework
        //

        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<MainProduct> MainProduct { get; set; }


}
}
