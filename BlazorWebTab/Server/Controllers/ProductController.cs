using Microsoft.AspNetCore.Mvc;

namespace BlazorWebTab.Server.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}