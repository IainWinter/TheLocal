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
        public IActionResult Login(string username, string passcode) {
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
                        hasher.VerifyHashedPassword(user, user.Passcode, passcode + pepper);

                    //Refresh hash if needed
                    if (pvr == PasswordVerificationResult.SuccessRehashNeeded) {
                        db.Users.Update(user);
                        user.Passcode = hasher.HashPassword(user, passcode + pepper);
                    } 
                    
                    if (pvr == PasswordVerificationResult.Success || pvr == PasswordVerificationResult.SuccessRehashNeeded) {
                        RandomGenerator random = new RandomGenerator(DateTime.UtcNow.Millisecond); //bad seed
                        string sessionString = random.GenerateString(32);

                        Session session =
                            (from s in db.Sessions
                             where s.Id == user.Id
                             select s).FirstOrDefault();

                        if (session == null) {
                            db.Sessions.Add(new Session { Id = user.Id, SessionId = sessionString });
                        } else {
                            db.Sessions.Update(session);
                            session.SessionId = sessionString;
                        }

                        db.SaveChanges();

                        Response.Cookies.Append("sessionid", sessionString);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string passcode) {
            using (var db = new MySqlDbContext()) {
                bool aUserAlready =
                   (from u in db.Users
                    where u.Username == username
                    select u).Any();

                if (!aUserAlready) {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                    string pepper = "P$t`bSo#yH{}R2o"; //Always the same, not stored in database. Store in appsettings.json

                    User user = new User {
                        Username = username
                    };

                    string hash = hasher.HashPassword(user, passcode + pepper);
                    user.Passcode = hash;

                    db.Users.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Login");
                }
            }

            return View();
        }
    }
}