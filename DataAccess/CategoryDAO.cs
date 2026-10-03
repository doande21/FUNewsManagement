using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock) { if (instance == null) instance = new CategoryDAO(); return instance; }
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using var db = new FUNewsManagementContext();
            return db.Categories.ToList();
        }

        public Category GetById(short id)
        {
            using var db = new FUNewsManagementContext();
            return db.Categories.Find(id);
        }

        public void Insert(Category category)
        {
            using var db = new FUNewsManagementContext();
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void Update(Category category)
        {
            using var db = new FUNewsManagementContext();
            db.Categories.Update(category);
            db.SaveChanges();
        }

        public void Delete(short id)
        {
            using var db = new FUNewsManagementContext();
            // Constraint: Không được xóa nếu chứa NewsArticle
            var hasNews = db.NewsArticles.Any(n => n.CategoryID == id);
            if (hasNews)
            {
                throw new Exception("Cannot delete this Category because it contains existing News Articles.");
            }

            var category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }
    }
}