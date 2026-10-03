using System;
using System.Collections.Generic;
using System.Text;
using BusinessObjects;
using DataAccess;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetAllCategories() => CategoryDAO.Instance.GetAll();
        public Category GetCategoryById(short id) => CategoryDAO.Instance.GetById(id);
        public void AddCategory(Category category) => CategoryDAO.Instance.Insert(category);
        public void UpdateCategory(Category category) => CategoryDAO.Instance.Update(category);
        public void DeleteCategory(short id) => CategoryDAO.Instance.Delete(id);
    }
}

