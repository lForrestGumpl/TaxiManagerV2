using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaxiManagerV2
{
   public class DB
    {
        protected static MySqlConnection connection;

        protected static void CreateConnection(string server, string bd, string login, string pass)
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.Database = "taximanager_db";
            sb.UserID = "root";
            sb.Password = "root";
            sb.CharacterSet = "utf8";
            connection = new MySqlConnection(sb.GetConnectionString(true));
        }
        protected static bool OpenConnection()
        {
            if (connection == null)
                CreateConnection(Properties.Settings.Default.server, Properties.Settings.Default.database, Properties.Settings.Default.login, Properties.Settings.Default.password);
            try
            {
                connection.Open();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return false;
        }
        protected static void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch (MySqlException e)
            {
               MessageBox.Show(e.Message);
            }
        }
        protected static int GetNextId(string table)
        {
            int id = 0;
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand("show table status where NAME = '" + table + "'", connection))
                {
                    using (MySqlDataReader dr = mc.ExecuteReader())
                    {
                        dr.Read();
                        id = dr.GetInt32("Auto_increment");
                    }
                }
                CloseConnection();
            }
            return id;
        }
        protected static bool RunSQL(string sql)
        {
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(sql, connection))
                    mc.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            return false;
        }

    }
}
