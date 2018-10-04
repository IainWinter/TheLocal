using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;

namespace TheLocal.Controllers {
    public class TestController : Controller {
        public IActionResult Test() {
            var name = new TestModel() { Name = "This is a test!!" };
            return View(name);
        }
    }
}