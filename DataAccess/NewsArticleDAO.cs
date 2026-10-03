using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class NewsArticleDAO
    {
        private static NewsArticleDAO instance = null;
        private static readonly object instanceLock = new object();
        private NewsArticleDAO() { }
        public static NewsArticleDAO Instance
        {
            get { lock (instanceLock) { if (instance == null) instance = new NewsArticleDAO(); return instance; } }
        }

        public IEnumerable<NewsArticle> GetAllActive()
        {
            using var db = new FUNewsManagementContext();
            return db.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy)
                     .Where(n => n.NewsStatus == true).OrderByDescending(n => n.CreatedDate).ToList();
        }

        public IEnumerable<NewsArticle> GetAllByStaff(short staffId)
        {
            using var db = new FUNewsManagementContext();
            return db.NewsArticles.Include(n => n.Category).Include(n => n.Tags)
                     .Where(n => n.CreatedByID == staffId).OrderByDescending(n => n.CreatedDate).ToList();
        }

        public IEnumerable<NewsArticle> GetReport(DateTime startDate, DateTime endDate)
        {
            using var db = new FUNewsManagementContext();
            return db.NewsArticles.Include(n => n.Category)
                     .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                     .OrderByDescending(n => n.CreatedDate).ToList();
        }

        public NewsArticle GetById(string id)
        {
            using var db = new FUNewsManagementContext();
            return db.NewsArticles.Include(n => n.Tags).FirstOrDefault(n => n.NewsArticleID == id);
        }

        public void Insert(NewsArticle news, List<int> tagIds)
        {
            using var db = new FUNewsManagementContext();
            news.CreatedDate = DateTime.Now;

            // Xử lý map tags
            if (tagIds != null && tagIds.Any())
            {
                var tags = db.Tags.Where(t => tagIds.Contains(t.TagID)).ToList();
                news.Tags = tags;
            }
            db.NewsArticles.Add(news);
            db.SaveChanges();
        }

        public void Update(NewsArticle news, List<int> tagIds)
        {
            using var db = new FUNewsManagementContext();
            var existingNews = db.NewsArticles.Include(n => n.Tags).FirstOrDefault(n => n.NewsArticleID == news.NewsArticleID);

            if (existingNews != null)
            {
                existingNews.NewsTitle = news.NewsTitle;
                existingNews.Headline = news.Headline;
                existingNews.NewsContent = news.NewsContent;
                existingNews.NewsSource = news.NewsSource;
                existingNews.CategoryID = news.CategoryID;
                existingNews.NewsStatus = news.NewsStatus;
                existingNews.ModifiedDate = DateTime.Now;
                existingNews.UpdatedByID = news.UpdatedByID;

                existingNews.Tags.Clear();
                if (tagIds != null && tagIds.Any())
                {
                    var tags = db.Tags.Where(t => tagIds.Contains(t.TagID)).ToList();
                    existingNews.Tags = tags;
                }
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using var db = new FUNewsManagementContext();
            var news = db.NewsArticles.Find(id);
            if (news != null)
            {
                db.NewsArticles.Remove(news);
                db.SaveChanges();
            }
        }

        public IEnumerable<Tag> GetAllTags()
        {
            using var db = new FUNewsManagementContext();
            return db.Tags.ToList();
        }
    }
}