using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TheLocal.Utility {
    public class MySqlConnectionHelper {
        private const string CONNECTION_STRING = "server=avidata.cymuktbsfffe.us-east-2.rds.amazonaws.com;" +
                                                 "user id=root;" +
                                                 "password=rootroot;" +
                                                 "pooling=False;" +
                                                 "database=TheLocal";
        public static IList<S> GetData<S>(string query) {
            IList<S> list = new List<S>();
            using (MySqlConnection connection = new MySqlConnection(CONNECTION_STRING)) {
                MySqlCommand command = new MySqlCommand(query, connection);
                try {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    foreach(S s in reader.Cast<S>()) {
                        list.Add(s);
                    }

                    reader.Close();
                } catch(MySqlException e) {
                    Console.WriteLine(e);
                } finally {
                    connection.Close();
                }
            }

            return list;
        }


        
    }
}
