using Microsoft.AspNetCore.Mvc;
using MyMvcApp.DataAccess.Data;
using MyMvcApp.Models;
using MyMvcApp.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMvcApp.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironmnent;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironmnent)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironmnent=webHostEnvironmnent;
        }

        public IActionResult Index()
        {
            List<Product> objProduct = _unitOfWork.Product.GetAll().ToList();
            return View(objProduct);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
                Product = new Product()
            };
            if(id== null || id==0)
            {
                return View(productVM);
            }
            else
            {
                //update
       productVM.Product=_unitOfWork.Product.Get(u=>u.Id==id);
      return View(productVM);
                }                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM , IFormFile? file )
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath= _webHostEnvironmnent.WebRootPath;
                if(file!=null){
                    string fileName =Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                    string productPath=Path.Combine(wwwRootPath,@"images/product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath=Path.Combine(wwwRootPath,productVM.Product.ImageUrl.TrimStart('/'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl=@"/images/product/"+fileName;
                }

                if(productVM.Product.Id==0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else{
                    _unitOfWork.Product.Update(productVM.Product);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "New Product Created Successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(productVM);
            }
        }

      

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully.";

            return RedirectToAction("Index");
        }
    }
}