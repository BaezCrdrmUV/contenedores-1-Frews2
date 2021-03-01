/*
    Date: 28/02/2021
    Author(s) : Ricardo Moguel Sánchez
 */

using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;


namespace DataAccess.DataBase
{
    public class DataBaseConnection
    {
        MySqlConnection connection;
        private string connectionString;
        private void Connect()
        {
            try
            {
                //connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                // connectionString = "server=localhost;port=3307;user=usuario;password=PASS333;database=inventario";
                connection = new MySqlConnection(connectionString);
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public MySqlConnection OpenConnection()
        {
            try
            {
                Connect();
            }
            catch (MySqlException)
            {
                throw;
            }

            return connection;
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                catch (MySqlException)
                {
                    throw;
                }
            }
        }
    }
}