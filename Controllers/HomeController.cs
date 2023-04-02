using System.Diagnostics;
using FirstCoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstCoreApp.Controllers
{
    public class HomeController : Controller
    {
        NewsContext db;
        public HomeController(NewsContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
         var result=db.Categories.ToList();
            return View(result);
        }
        public IActionResult News(int id)
        {
            Category c=db.Categories.Find(id);
            ViewBag.Cat = c.Name;
            //ViewData["Cat"] = c.Name;
            var result = db.News.Where(x=>x.CatrgoryId==id).OrderByDescending(y=>y.Date).ToList();
            return View(result);
        }
        public IActionResult DeleteNews(int id)
        {
            var oneNews=db.News.Find(id);
            db.News.Remove(oneNews);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Comments()
        {
            return View(db.Contacts.ToList());
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveContact(ContactUS model)
        {
            db.Contacts.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");   
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}