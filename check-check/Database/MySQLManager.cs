using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace check_check.Database
{
    class MySQLManager
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;

        public MySQLManager()
        {
            Initialize();
        }

        private void Initialize()
        {
            Console.WriteLine("Database Initialize");

            string connectionString;
            string server = "localhost";
            string database = "checkcheck_db";
            string username = XmlManager.GetValue("DATABASE", "MYSQL_USERNAME");
            string password = XmlManager.GetValue("DATABASE", "MYSQL_PASSWORD");

            connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={password};";

            conn = new MySqlConnection(connectionString);
        }

        /*       public void Connection()
               {
                   try
                   {
                       conn.Open();
                       Console.WriteLine("Connection to database successful");
                   }
                   catch (MySqlException ex)
                   {
                       switch (ex.Number)
                       {
                           case 0:
                               MessageBox.Show("Unable to connect to database server");
                               break;

                           case 1045:
                               MessageBox.Show("Please check your ID or password.");
                               break;
                       }
                   }
               }
        */
        public void Insert(string table, string columns, string value)
        {
            try
            {
                conn.Open();
                string query = $"INSERT INTO {table} ({columns}) VALUES ({value})";
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public DataSet SelectAll(string table)
        {
            try
            {
                DataSet dataset = new DataSet();

                string query = $"SELECT * FROM {table}";
                adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dataset, table);
                if (dataset.Tables.Count > 0)
                {
                    return dataset;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
    }
}
