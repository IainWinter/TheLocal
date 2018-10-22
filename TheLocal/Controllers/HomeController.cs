using Microsoft.AspNetCore.Mvc;
using TheLocal.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TheLocal.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ViewResult Index() {
            IList<Name> list = new List<Name>();

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder {
                DataSource = "avidata.cymuktbsfffe.us-east-2.rds.amazonaws.com",
                UserID = "root",
                Password = "rootroot",
                Pooling = false
            };

            SqlConnection connection = new SqlConnection(sb.ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM users;", connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            string first = reader.GetString(1);
            string last = reader.GetString(2);

            reader.Close();
            list.Add(new Name() { First = first, Last = last });
            return View(list);
        }

        [HttpPost]
        public ViewResult Index(string first, string last) {
            Name name = new Name() { First = first, Last = last };
            return View(name);
        }
    }
}
