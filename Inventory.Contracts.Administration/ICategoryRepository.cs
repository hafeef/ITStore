using Inventory.ViewModels.Administration;
using System.Collections.Generic;

namespace Inventory.Contracts.Administration
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetAllCategory();
        void CreateNewCategory(CategoryVM newCategory);
        void DeleteCategory(int id);
        List<CategoryVM> SearchCategoryByName(string categoryName);
        void UpdateCategory(CategoryVM category);

    }
}
