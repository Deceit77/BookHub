using Microsoft.AspNetCore.Mvc;
using MyMvcApp.DataAccess.Data;
using MyMvcApp.Models;
using MyMvcApp.Utility;
using Microsoft.AspNetCore.Authorization;
using MyMvcApp.DataAccess.Repository.IRepository;
using MyMvcApp.DataAccess.Repository;
namespace MyMvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
         public IActionResult Index()
    {
        List<Category> objCategory= _unitOfWork.Category.GetAll().ToList();
        return View(objCategory);
    }
         public IActionResult Create()
         {
              return View();
         }
         [HttpPost]
          public IActionResult Create(Category obj)
         {
            if(ModelState.IsValid){
            _unitOfWork.Category.Add(obj);
           _unitOfWork.Save();
            TempData["success"]="New Category Created Succesfully.";
            }
           return RedirectToAction("Index");
         }
          public IActionResult Edit( int? id)
         { 
            if(id==null || id==0)
            {
                return NotFound();
            }
              Category? categoryFromDb=_unitOfWork.Category.Get(u=>u.CategoryId==id);
              if(categoryFromDb==null){
                  return NotFound();
              }
              return View(categoryFromDb);
         }
         [HttpPost]
          public IActionResult Edit(Category obj)
         {
            if(ModelState.IsValid)
            {
              _unitOfWork.Category.Update(obj);
           _unitOfWork.Save();
            TempData["success"]="Category Updated Succesfully.";
            }
           return RedirectToAction("Index");
         }
            public IActionResult Delete( int? id)
         { 
            if(id==null || id==0)
            {
                return NotFound();
            }
              Category? categoryFromDb=_unitOfWork.Category.Get(u=>u.CategoryId==id);
              if(categoryFromDb==null){
                  return NotFound();
              }
              return View(categoryFromDb);
         }

          [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _unitOfWork.Category.Get(u=>u.CategoryId==id);
            if (obj == null)
            {
                return NotFound();
            }

           _unitOfWork.Category.Remove(obj);
         _unitOfWork.Save();
            TempData["success"]="Category Deleted Succesfully.";

            return RedirectToAction("Index");
        }
    }
}