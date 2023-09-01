using BL;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers;

public class PostsController : Controller
{
    PostManager postManager = new PostManager();
    public IActionResult Index(int? id)
    {
        if (id != null)
        {
            return View(postManager.GetAll(x => x.CategoryId == id));
        }

        return View(postManager.GetAll());
    }
    public IActionResult Detail(int id)
    {
        return View(postManager.Find(id));
    }
}
