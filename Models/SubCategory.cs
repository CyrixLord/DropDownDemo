using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DropDownDemo.Models
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryID { get; set; }

        public int CategoryID { get; set; }

        public string SubCategoryName { get; set; }
    }
}
