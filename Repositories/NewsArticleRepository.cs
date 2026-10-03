using BusinessObjects;
using DataAccess;

namespace Repositories
{
    public interface INewsArticleRepository
    {
        IEnumerable<NewsArticle> GetAllActive();
        IEnumerable<NewsArticle> GetAllByStaff(short staffId);
        IEnumerable<NewsArticle> GetReport(DateTime startDate, DateTime endDate);
        NewsArticle GetById(string id);
        void Insert(NewsArticle news, List<int> tagIds);
        void Update(NewsArticle news, List<int> tagIds);
        void Delete(string id);
        IEnumerable<Tag> GetAllTags();
    }

    public class NewsArticleRepository : INewsArticleRepository
    {
        public IEnumerable<NewsArticle> GetAllActive() => NewsArticleDAO.Instance.GetAllActive();
        public IEnumerable<NewsArticle> GetAllByStaff(short staffId) => NewsArticleDAO.Instance.GetAllByStaff(staffId);
        public IEnumerable<NewsArticle> GetReport(DateTime startDate, DateTime endDate) => NewsArticleDAO.Instance.GetReport(startDate, endDate);
        public NewsArticle GetById(string id) => NewsArticleDAO.Instance.GetById(id);
        public void Insert(NewsArticle news, List<int> tagIds) => NewsArticleDAO.Instance.Insert(news, tagIds);
        public void Update(NewsArticle news, List<int> tagIds) => NewsArticleDAO.Instance.Update(news, tagIds);
        public void Delete(string id) => NewsArticleDAO.Instance.Delete(id);
        public IEnumerable<Tag> GetAllTags() => NewsArticleDAO.Instance.GetAllTags();
    }
}