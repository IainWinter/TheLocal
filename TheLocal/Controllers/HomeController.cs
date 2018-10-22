using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace TheLocal.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ViewResult Index() {
            IList<Name> list = new List<Name>();

            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder {
                Server = "avidata.cymuktbsfffe.us-east-2.rds.amazonaws.com",
                UserID = "root",
                Password = "rootroot",
                Pooling = false,
                Database = "TheLocal"
            };

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);
            MySqlCommand command = new MySqlCommand("SELECT * FROM users;", connection);
            connection.Open();

            MySqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            
            while(reader.Read()) {
                string first = reader.GetString(1);
                string last = reader.GetString(2);

                list.Add(new Name() { First = first, Last = last });
            }

            reader.Close();
            return View(list);
        }

        [HttpPost]
        public RedirectToActionResult Index(string first, string last) {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder {
                Server = "avidata.cymuktbsfffe.us-east-2.rds.amazonaws.com",
                UserID = "root",
                Password = "rootroot",
                Pooling = false,
                Database = "TheLocal"
            };

            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);
            MySqlCommand command = new MySqlCommand($"INSERT INTO users (first, last) values('{first}', '{last}');", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index");
        }
    }
}
