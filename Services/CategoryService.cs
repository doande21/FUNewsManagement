using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Category> GetAllCategories() => _repo.GetAllCategories();
        public Category GetCategoryById(short id) => _repo.GetCategoryById(id);
        public void AddCategory(Category category) => _repo.AddCategory(category);
        public void UpdateCategory(Category category) => _repo.UpdateCategory(category);
        public void DeleteCategory(short id) => _repo.DeleteCategory(id);
    }
}

