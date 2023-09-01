using BL;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.ViewComponents;

public class CategoriesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        CategoryManager categoryManager = new CategoryManager();

        return View(categoryManager.GetAll());
    }
}
