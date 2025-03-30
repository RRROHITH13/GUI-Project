using System;
using MySql.Data.MySqlClient;

namespace DB_Connection
{
    class Db_connection
    {
        public MySqlConnection connectDB()
        {
            string connect_string = "Server=localhost; Database=uservs; User ID=root; Password=root;";
            MySqlConnection con = new MySqlConnection(connect_string);
            return con;
        }
    }
}
