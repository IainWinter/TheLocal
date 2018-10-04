using Microsoft.AspNetCore.Mvc;

namespace TheLocal.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ViewResult Index() {
            return View();
        }

        [HttpPost]
        public ContentResult Index(string first, string last) {
            return Content($"Hello {first}, {last}!");
        }
    }
}
