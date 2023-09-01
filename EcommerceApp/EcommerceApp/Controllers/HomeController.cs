using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BL;
using Entities;
using EcommerceApp.Utility;

namespace EcommerceApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    CategoryManager categoryManager = new CategoryManager();
    SliderManager sliderManager = new SliderManager();
    NewsManager newsManager = new NewsManager();
    PostManager postManager = new PostManager();
    ContactManager contactManager = new ContactManager();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new HomePageViewModel()
        {
            SliderList = sliderManager.GetAll(),
            CategoryList = categoryManager.GetAll(),
            NewsList = newsManager.GetAll(),
            PostList = postManager.GetAll(),
        };
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Contact(Contact contact)
    {
        if (ModelState.IsValid)
        {
            try
            {
                contact.CreateDate = DateTime.Now;
                var mailsent = MailHelper.SendMail(contact);
                var result = contactManager.Add(contact);
                if (result > 0)
                {
                    TempData["Message"] = "Message was submited succesfully";
                    return RedirectToAction(nameof(Contact));
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Something went wrong! Message was't submitted";
                throw;
            }
            
        }
        return View(contact);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}