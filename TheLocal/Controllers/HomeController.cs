using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TheLocal.Utility;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TheLocal.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ViewResult Index() {
            User user;
            using (var db = new MySqlDbContext()) {
                db.Database.EnsureCreated();

                user =
                   (from s in db.Sessions
                    join u in db.Users on s.Id equals u.Id
                    where s.SessionId == Request.Cookies["sessionid"]
                    select u).FirstOrDefault();
            }

            return View(user);
        }

        //Signup
        //[HttpPost]
        //public ActionResult Login(string username, string passcode) {
        //    using (var db = new MySqlDbContext()) {
        //        db.Database.EnsureCreated();

        //        RandomGenerator random = new RandomGenerator(DateTime.UtcNow.Millisecond); //bad seed

        //        string salt = random.GenerateString(15); //Random
        //        string pepper = "P$t`bSo#yH{}R2o"; //Always the same, not stored in database. Store in appsettings.json

        //        //Hash salt | pepper | passcode
        //        SHA256 sha256 = SHA256.Create(); //bad hash use argon2
        //        byte[] bytes = Encoding.UTF8.GetBytes(salt + pepper + passcode);
        //        byte[] hash = sha256.ComputeHash(bytes);

        //        User u = new User {
        //            Username = username,
        //            Passcode = hash,
        //            Salt = salt
        //        };



        //        db.Users.Add(u);

        //        db.SaveChanges();
        //    }

        //    return Content("You've done it now.");
        //}
    }
}