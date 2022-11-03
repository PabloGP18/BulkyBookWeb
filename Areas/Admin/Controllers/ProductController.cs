using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // making the connection true the dependency injection in the controller 
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _hostEnvironment;

        // making the constructor for the connection true the dependency injection in the controller

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }


        // READ
        public IActionResult Index()
        {
            // calling DB information table categories
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
            return View(objProductList);
        }
      


            // UPDATE
        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                 Product = new(),
                // this is how you can use projections with the .Select 
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };

          
            // Id has to exist or can not be 0
            if (id == null || id == 0)
            {
                // create product
                // ViewBag is to transfer something from controller to view and not vice-versa
                // ViewBag is ideal for temporary data that is not in a model
                // ViewBag's life only lasts during the current http request. The value will be null if redirection occurs.
                //ViewBag.CategoryList = CategoryList;
                // Using viewData instead of the ViewBag. They are both similar. With ViewData you have to assign a key and also CAST it in the view.
                // The key of ViewData and the property of ViewBag must not match because viewbag inserts data into viewdata dictionarry
                //ViewData["CoverTypeList"]= CoverTypeList;
                // You have also TempData => uses session to store the data.
                // TempData can be used to store only one time messages like error messages, validation messages...
                return View(productVM);
            }
            else
            {
                //Update product
            }
            return View(productVM);
        }


        //POST
        [HttpPost]

        [ValidateAntiForgeryToken]
        // IFormFile you need to be able to upload a file in the view
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {

          

            if (ModelState.IsValid)
            {
                // This is to get the roothpath 
                string wwwRootPath = _hostEnvironment.WebRootPath;
                // if the file is not null
                if (file != null)
                {
                    // This is to not use the existing name => this way it is renamed and uploaded
                    string fileName = Guid.NewGuid().ToString();
                    // this is where the images will be stored => you have to make a variable and then combine the path
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    // The file will be renamed, but we want to keep the same extension
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create)) 
                    {
                        // This is copy the file to the fileStreams
                        file.CopyTo(fileStreams);
                    }
                    // This is what will be saved in the DB
                    obj.Product.ImagerUrl = @"\images\products" + fileName + extension;
                }
                // Based on the primary key the update wil automaticly update all of the properties
                //_unitOfWork.CoverType.Update(obj);

                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);

        }




        // DELETE 


        // Get

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            // var CoverTypeFromDB = _db.Categories.Find(id);
            var CoverTypeFromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFromDBFirst == null)
            {
                return NotFound();
            }

            return View(CoverTypeFromDBFirst);
        }




        // Post
        // With the action name you can give DeletePost another name for in the delete view
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(obj);

            _unitOfWork.Save();
            // Tempdata is a temporary message that will be in the view untill you refresh the page => look @ the _layout file (partial) how to implement them!
            TempData["success"] = "CoverType deleted successfully";


            return RedirectToAction("Index");
        }

    }
}



