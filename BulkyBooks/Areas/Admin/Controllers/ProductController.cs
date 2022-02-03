using BulkyBooks.DataAccess;
using BulkyBooks.DataAccess.Repository.IRepository;
using BulkyBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBooks.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypelist = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypelist);
        }
        

        public IActionResult Upsert(int? id)
        {
            Product product = new();
            if(id == null || id == 0)
            {
                // Create Product
                return View(product);
            }
            else
            {
                // Update Product
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFromDB = _unitOfWork.CoverType.GetFirstOrDefault(u=>u.Id ==id);
            if (CoverTypeFromDB == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDB);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
