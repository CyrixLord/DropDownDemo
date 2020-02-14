using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DropDownDemo.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DropDownDemo.Controllers
{
    public class DemoController : Controller
    {
        private readonly DatabaseContext _context; // lets use the database context we created in startup and use 
                                                   // dependency injection to get that context that refers to the database
        public DemoController(DatabaseContext context) // introduce a constructor that takes the context available to this class
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Category> categorylist = new List<Category>(); // create a list to hold the category names

            // get the data from the database using entity framework. I would normally build a repo assist and get it that way
            // but this is a small demo

            categorylist = (from category in _context.Category  // lets do a fancy jquery query to get all the categories
                            select category).ToList();          // and put it in our newly instantaniated category list

            // insert an item into the list as the first item and call it 'Select'

            categorylist.Insert(0, new Category { CategoryID = 0, CategoryName = "Select" });

            // get sloppy and throw the category list in the viewbag. we would do things cleaner using a ViewModel 
            // and send that to the form and not use the viewbag. but again this is a small demo to learn about
            // making dynamic dropdowns and using ajax to populate them based on the first dropdown's selection

            ViewBag.ListofCategory = categorylist;
            return View();
        }
    }
}
