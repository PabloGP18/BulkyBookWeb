using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        // making the connection true the dependency injection in the controller 
        private readonly ApplicationDbContext _db;

        // making the constructor for the connection true the dependency injection in the controller

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // calling DB information table categories
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {  
            return View();
        }


        //POST
        [HttpPost]
        // This is needed so that they can't hack the website true the form
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            // custom validation => we want to make sure we do not add any category wich has the same name and display order
            // you need summary validation to display this without giving a key name of the form
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                // adding a custom error
                // If you want to add the orrer to the form and not to the summary validation => change key CustomError to Name or display order to display validation in form
                ModelState.AddModelError("CustomError", "The DisplayOrder cannot exactly match the Name.");
            }
            // Check validations serverside => modalstate.isvalid
            if (ModelState.IsValid)
            {
                // this is a command to add something to the DB
                _db.Categories.Add(obj);
                // adding saveChanges it wil go to the DB and save the changes
                _db.SaveChanges();

                // with redirectaction you can go back to the index instead of staying on the page
                // This will look for the index action inside the same controller
                // if you had to redirect to another controller, you can just do it like this example index HomeController ("index","HomeController")
                return RedirectToAction("Index");
            }
            // if you don't fill in nothing in create category, it will return the view.
            return View(obj);

        }
    }
}
