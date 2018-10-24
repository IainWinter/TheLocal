using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using TheLocal.Utility;

namespace TheLocal.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ViewResult Index() {
            string q = "SELECT u.username, p.title, p.post, p.date FROM posts p inner join users u on p.id = u.id";
            return View(MySqlConnectionHelper.GetData<Post>(q));


            //IList<Post> list = new List<Post>();

            //MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder {
            //    Server   = "avidata.cymuktbsfffe.us-east-2.rds.amazonaws.com",
            //    UserID   = "root",
            //    Password = "rootroot",
            //    Pooling  = false,
            //    Database = "TheLocal"
            //};

            //string command_str = "SELECT u.username, p.title, p.post, p.date FROM posts p inner join users u on p.id = u.id";

            //MySqlConnection connection = new MySqlConnection(sb.ConnectionString);
            //MySqlCommand command = new MySqlCommand(command_str, connection);
            //connection.Open();

            //MySqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            //while (reader.Read()) {
            //    string user = reader.GetString(0);
            //    string title = reader.GetString(1);
            //    string text  = reader.GetString(2);
            //    DateTime datetime = reader.GetDateTime(3);

            //    Name n = new Name() { First = user, Last = "" };
            //    Post p = new Post() { Title = title, Text = text, User = n, Datetime = datetime };
            //    list.Add(p);
            //}

            //reader.Close();
            //return View(list);
        }

        [HttpGet]
        public ViewResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass) {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder {
                Server   = "avidata.cymuktbsfffe.us-east-2.rds.amazonaws.com",
                UserID   = "root",
                Password = "rootroot",
                Pooling  = false,
                Database = "TheLocal"
            };

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);
            MySqlCommand command = new MySqlCommand($"INSERT INTO users (username, pass) values('{user}', '{pass}');", connection);

            ActionResult result;
            try {
                connection.Open();
                command.ExecuteNonQuery();
                result = RedirectToAction("Index");
            } catch(Exception) {
                result = Content("Username is already taken!");
            } finally {
                connection.Close();
            }

            Response.Cookies.Append("Name", user);
            return result;
        }
    }
}
