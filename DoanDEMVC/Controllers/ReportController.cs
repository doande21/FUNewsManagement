// Thư mục: DoanDEMVC/Controllers/ReportController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DoanDEMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly INewsArticleService _newsService;

        public ReportController(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                return View(new List<BusinessObjects.NewsArticle>());
            }

            var reportData = _newsService.GetReport(startDate.Value, endDate.Value);
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(reportData);
        }
    }
}