using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TheLocal.Utility;
using System.Linq;

namespace TheLocal.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ViewResult Index() {
            IList<Post> posts;
            using (var db = new MySqlDbContext()) {
                db.Database.EnsureCreated();

                IQueryable<Post> result = 
                    from post in db.Posts
                    join user in db.Users on post.Id equals user.Id
                    select new Post {
                        Title = post.Title,
                        Text = post.Text,
                        User = user.Username,
                        Date = post.Date
                    };

                posts = new List<Post>(result);
            }

            return View(posts);
        }

        [HttpGet]
        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string passcode) {
            using (var db = new MySqlDbContext()) {
                db.Database.EnsureCreated();

                RandomGenerator random = new RandomGenerator(DateTime.UtcNow.Millisecond); //bad seed

                string salt = random.GenerateString(15); //Random
                string pepper = "P$t`bSo#yH{}R2o"; //Always the same, not stored in database. Store in appsettings.json

                //Hash salt | pepper | passcode
                SHA256 sha256 = SHA256.Create(); //bad hash use argon2
                byte[] bytes = Encoding.UTF8.GetBytes(salt + pepper + passcode);
                byte[] hash = sha256.ComputeHash(bytes);

                User u = new User {
                    Username = username,
                    Passcode = hash,
                    Salt = salt
                };

                db.Users.Add(u);

                db.SaveChanges();
            }

            return Content("You've done it now.");
        }
    }
}
