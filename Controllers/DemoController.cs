using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DropDownDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        
        // action methods
        // we will use JQUERY and AJAX to populate the dropdown boxes with the methods below. they must be returned
        // in JSON format (JsonResult) to the view.
        // ADDING GetSubcategories Method to Demo Controller:

        public JsonResult GetSubCategory(int CategoryID)
        {
            List<SubCategory> subCategorylist = new List<SubCategory>();

            // get the data from the database using EntityFramework core and put the results into a list

            subCategorylist = (from subcategory in _context.SubCategory
                               where subcategory.CategoryID == CategoryID
                               select subcategory).ToList();

            // we still need to have 'Select' option in the list so we should insert that into the list before we
            // return it

            subCategorylist.Insert(0, new SubCategory { SubCategoryID = 0, SubCategoryName = "Select" });

            return Json(new SelectList(subCategorylist, "SubCategoryID", "SubCategoryName"));
        }

        // adding GetProducts method to Demo Controller

        public JsonResult GetProducts(int SubCategoryID)
        {
            List<MainProduct> productList = new List<MainProduct>();

            // AJAX will call this controller method to get data from the database
            productList = (from product in _context.MainProduct
                           where product.SubCategoryID == SubCategoryID
                           select product).ToList();

            // again, add the select option at the start of the list

            productList.Insert(0, new MainProduct {  ProductID = 0, ProductName = "Select"});

            return Json(new SelectList(productList, "ProductID", "ProductName"));
        }

        [HttpPost]
        public IActionResult Index(Category objcategory, IFormCollection formCollection)
        {
            if (objcategory.CategoryID == 0)
            {
                ModelState.AddModelError("", "Select Category");
            }
            else if (objcategory.SubCategoryID == 0)
            {
                ModelState.AddModelError("", "Select SubCategory");
            }
            else if (objcategory.ProductID == 0)
            {
                ModelState.AddModelError("", "Select Product");
            }

            // get selected value

            var SubCategoryID = HttpContext.Request.Form["SubcategoryID"].ToString();
            var ProductID = HttpContext.Request.Form["ProductID"].ToString();

            // set data back to viewbag after posting form

            List<Category> categorylist = new List<Category>();
            categorylist = (from category in _context.Category
                            select category).ToList();
            categorylist.Insert(0, new Category { CategoryID = 0, CategoryName = "Select" });

            // assign category to viewbag list of category
            ViewBag.ListofCategory = categorylist;
            return View(objcategory);

        }
    }
}
