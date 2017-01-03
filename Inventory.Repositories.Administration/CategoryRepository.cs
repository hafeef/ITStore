using Inventory.Contracts.Administration;
using Inventory.Data.Administration;
using Inventory.DomainClasses.Administration;
using Inventory.ViewModels.Administration;
using System;
using System.Collections.Generic;

namespace Inventory.Repositories.Administration
{
    public class CategoryRepository : DataRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AdminContext context) : base(context)
        {

        }

        public void CreateNewCategory(CategoryVM newCategory)
        {
            try
            {
                Insert(AutoMapper.Mapper.Map<Category>(newCategory));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void DeleteCategory(int categoryID)
        {
            try
            {
                Delete(categoryID);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public List<CategoryVM> GetAllCategory()
        {
            try
            {
                var categories = FindBy(category => category.IsActive == true);
                return AutoMapper.Mapper.Map<List<Category>, List<CategoryVM>>(categories);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<CategoryVM> SearchCategoryByName(string categoryName)
        {
            try
            {
                var categories = FindBy(category => category.IsActive == true && category.Name.Contains(categoryName));
                return AutoMapper.Mapper.Map<List<Category>, List<CategoryVM>>(categories);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateCategory(CategoryVM category)
        {
            try
            {
                var dbCategory = FindByKey(category.CategoryID);
                dbCategory = AutoMapper.Mapper.Map(category, dbCategory);
                Update(dbCategory);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
