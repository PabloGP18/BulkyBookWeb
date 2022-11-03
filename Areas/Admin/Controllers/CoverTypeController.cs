using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        // making the connection true the dependency injection in the controller 
        private readonly IUnitOfWork _unitOfWork;

        // making the constructor for the connection true the dependency injection in the controller

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // READ
        public IActionResult Index()
        {
            // calling DB information table categories
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }





        // POST

        //GET
        public IActionResult Create()
        {
            return View();
        }


        //POST
        [HttpPost]
        // This is needed so that they can't hack the website true the form
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {

            // Check validations serverside => modalstate.isvalid
            if (ModelState.IsValid)
            {
                // this is a command to add something to the DB
                _unitOfWork.CoverType.Add(obj);
                // adding saveChanges it wil go to the DB and save the changes
                _unitOfWork.Save();
                TempData["success"] = "CoverType created successfully";
                // with redirectaction you can go back to the index instead of staying on the page
                // This will look for the index action inside the same controller
                // if you had to redirect to another controller, you can just do it like this example index HomeController ("index","HomeController")
                return RedirectToAction("Index");
            }
            // if you don't fill in nothing in create CoverType, it will return the view.
            return View(obj);








            // UPDATE
        }
        //GET
        // Edit will display the existing functionality of the CoverType that was selected.
        // Here we will retrieve an intiger that will be id
        public IActionResult Edit(int? id)
        {
            // Id has to exist or can not be 0
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // Here we extract the id from de db with the find method. This way it tries to find the primary key based on the primary key of the table and assigned that to the variable CoverTypeFromDB.
            //var CoverType = _db.Categories.Find(id);

            // 2 other ways to retrieve an ID from CoverType

            // Here it will find the Id and it will return the first one it finds with the id you arsking for.
            var CoverTypeFromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            // it returns only one element but without throwing an exeption
            // var CoverTypeFromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id==id);

            // If it doesn't find a CoverType it will display not found
            if (CoverTypeFromDBFirst == null)
            {
                return NotFound();
            }
            // If it founds the CoverType it will return it to the view
            return View(CoverTypeFromDBFirst);
        }


        //POST
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {

          

            if (ModelState.IsValid)
            {
                // Based on the primary key the update wil automaticly update all of the properties
                _unitOfWork.CoverType.Update(obj);

                _unitOfWork.Save();
                TempData["success"] = "CoverType updated successfully";


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



