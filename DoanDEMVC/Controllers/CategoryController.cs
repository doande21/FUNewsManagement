using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DoanDEMVC.Controllers
{
    // Chỉ Admin và Staff (Role = 1) được quản lý Category
    [Authorize(Roles = "Admin,Staff")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        // Trả về Partial View cho Modal Create/Edit
        [HttpGet]
        public IActionResult GetCategoryForm(short? id)
        {
            if (id == null) return PartialView("_CategoryForm", new Category());

            var category = _categoryService.GetCategoryById(id.Value);
            if (category == null) return NotFound();

            return PartialView("_CategoryForm", category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryID == 0)
                {
                    _categoryService.AddCategory(category);
                }
                else
                {
                    _categoryService.UpdateCategory(category);
                }
                return Json(new { success = true }); // Trả về JSON để AJAX load lại trang
            }
            return PartialView("_CategoryForm", category);
        }

        [HttpPost]
        public IActionResult Delete(short id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                TempData["SuccessMsg"] = "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = ex.Message; // Hiển thị lỗi "Cannot delete..."
            }
            return RedirectToAction(nameof(Index));
        }
    }
}