// Thư mục: DoanDEMVC/Controllers/NewsArticleController.cs
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using System.Security.Claims;
using Services;

namespace DoanDEMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsService;
        private readonly ICategoryService _categoryService;

        public NewsArticleController(INewsArticleService newsService, ICategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        // Action chung cho Guest / Lecturer / Staff xem danh sách bài viết
        [AllowAnonymous]
        public IActionResult Index(string searchKeyword, short? categoryId)
        {
            IEnumerable<NewsArticle> articles;

            // Nếu là Staff (Role = 1), xem lịch sử bài viết mình tạo
            if (User.IsInRole("Staff"))
            {
                short staffId = short.Parse(User.FindFirst("AccountId").Value); // Lấy động từ Cookie
                articles = _newsService.GetAllByStaff(staffId);
            }
            else
            {
                // Khách, Lecturer, Admin -> Xem tin Active
                articles = _newsService.GetAllActive();
            }

            // Filter (Search)
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                articles = articles.Where(a => a.NewsTitle.Contains(searchKeyword, StringComparison.OrdinalIgnoreCase)
                                            || a.Headline.Contains(searchKeyword, StringComparison.OrdinalIgnoreCase));
            }
            if (categoryId.HasValue)
            {
                articles = articles.Where(a => a.CategoryID == categoryId.Value);
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName");
            return View(articles);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public IActionResult GetNewsForm(string id)
        {
            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName");
            ViewBag.Tags = _newsService.GetAllTags(); // Trả về List<Tag> cho Checkbox nhiều lựa chọn

            if (string.IsNullOrEmpty(id)) return PartialView("_NewsArticleForm", new NewsArticle());

            var news = _newsService.GetById(id);
            if (news == null) return NotFound();

            // Đưa list TagID hiện tại vào ViewBag để check checkbox
            ViewBag.SelectedTags = news.Tags.Select(t => t.TagID).ToList();
            return PartialView("_NewsArticleForm", news);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNews(NewsArticle news, List<int> selectedTags)
        {
            if (ModelState.IsValid)
            {
                news.UpdatedByID = 3; // Gán ID Staff đang đăng nhập (Lấy từ Claim thực tế)

                var existingNews = _newsService.GetById(news.NewsArticleID);
                if (existingNews == null)
                {
                    news.CreatedByID = 3;
                    _newsService.Insert(news, selectedTags);
                }
                else
                {
                    _newsService.Update(news, selectedTags);
                }
                return Json(new { success = true });
            }

            ViewBag.Categories = new SelectList(_categoryService.GetAllCategories(), "CategoryID", "CategoryName");
            ViewBag.Tags = _newsService.GetAllTags();
            return PartialView("_NewsArticleForm", news);
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            _newsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}