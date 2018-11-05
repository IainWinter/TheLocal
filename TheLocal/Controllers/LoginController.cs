using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;
using TheLocal.Utility;

namespace TheLocal.Controllers {
    public class LoginController : Controller {
        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public ContentResult Login(string username, string passcode) {
            using (var db = new MySqlDbContext()) {
                db.Database.EnsureCreated();

                User user =
                   (from u in db.Users
                    where u.Username == username
                    select u).FirstOrDefault();

                if (user != null) {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                    string pepper = "P$t`bSo#yH{}R2o"; //Always the same, not stored in database. Store in appsettings.json

                    PasswordVerificationResult pvr =
                        hasher.VerifyHashedPassword(user, user.Passcode, user.Passcode + pepper);

                    //Refresh hash if needed
                    if (pvr == PasswordVerificationResult.SuccessRehashNeeded) {
                        db.Users.Update(user);
                        user.Passcode = hasher.HashPassword(user, passcode + pepper);
                    } 
                    
                    else
                    if (pvr == PasswordVerificationResult.Success) {
                        RandomGenerator random = new RandomGenerator(DateTime.UtcNow.Millisecond); //bad seed
                        string sessionString = random.GenerateString(16);

                        Session session =
                            (from s in db.Sessions
                             where s.Id == user.Id
                             select s).FirstOrDefault();

                        if (session == null) {
                            db.Sessions.Add(new Session { Id = user.Id, SessionId = Encoding.UTF8.GetBytes(sessionString) });
                            db.SaveChanges();
                        } else {
                            db.Sessions.Update(session);
                            session.SessionId = Encoding.UTF8.GetBytes(sessionString);
                        }

                        return Content($"{username} has been logged in!");
                    }
                }
            }

            return Content($"{username} has failed to login!");
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public ContentResult Register(string username, string passcode) {
            return Content($"{username} has been registered!");
        }
    }
}