using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;

namespace TheLocal.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ViewResult Index() {
            return View();
        }

        [HttpPost]
        public ViewResult Index(string first, string last) {
            Name name = new Name() { First = first, Last = last };
            return View(name);
        }
    }
}
