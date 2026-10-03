using BusinessObjects;
using Repositories;

namespace Services
{
    public interface INewsArticleService
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

    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _repo;

        public NewsArticleService(INewsArticleRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<NewsArticle> GetAllActive() => _repo.GetAllActive();
        public IEnumerable<NewsArticle> GetAllByStaff(short staffId) => _repo.GetAllByStaff(staffId);
        public IEnumerable<NewsArticle> GetReport(DateTime startDate, DateTime endDate) => _repo.GetReport(startDate, endDate);
        public NewsArticle GetById(string id) => _repo.GetById(id);
        public void Insert(NewsArticle news, List<int> tagIds) => _repo.Insert(news, tagIds);
        public void Update(NewsArticle news, List<int> tagIds) => _repo.Update(news, tagIds);
        public void Delete(string id) => _repo.Delete(id);
        public IEnumerable<Tag> GetAllTags() => _repo.GetAllTags();
    }
}