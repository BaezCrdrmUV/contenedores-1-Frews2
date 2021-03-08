/*
    Date: 28/02/2021
    Author(s) : Ricardo Moguel Sánchez
 */
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dominio;
using System;

namespace AccesoDatos
{
    class TelefonoDAO
    {
        private List<Telefono> telefonos;
        private Telefono telefono;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public TelefonoDAO()
        {
            connection = new DataBaseConnection();
        }

        public List<Telefono> ConsultarTelefonosPorIDPersona(int idPersona)
        {
            try
            {
                telefonos = new List<Telefono>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT numero_Telefono FROM Direcciones WHERE idDuenio = @idDuenio "
                };

                MySqlParameter idDuenio = new MySqlParameter("@idDuenio", MySqlDbType.Int32, 11)
                {
                    Value = idPersona
                };

                query.Parameters.Add(idPersona);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    telefono = new Telefono
                    {
                        NumeroTelefono = reader.GetString(0)
                    };

                    telefonos.Add(telefono);
                }

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Ocurrio un error al conectar a base de datos", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return telefonos;
        }


        public bool AltaTelefono(int idPersona, Telefono telefono)
        {
            bool seGuardo = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Telefono(numero_Telefono, idDuenio)" +
                    " VALUES (@numero_Telefono, @idDuenio) "
                };

                MySqlParameter numeroTelefono = new MySqlParameter("@numero_Telefono", MySqlDbType.VarChar, 10)
                {
                    Value = telefono.NumeroTelefono

                };

                MySqlParameter idDuenio = new MySqlParameter("@idDuenio", MySqlDbType.Int32, 11)
                {
                    Value = idPersona
                };

                query.Parameters.Add(numeroTelefono);
                query.Parameters.Add(idDuenio);

                query.ExecuteNonQuery();
                seGuardo = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Ocurrio un error al conectar a base de datos", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return seGuardo;
        }

        public bool ActualizarTelefono(int idPersona, Telefono telefono)
        {
            bool seActualizo = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Telefonos SET numero_Telefono = @numero_Telefono " + 
                    "WHERE idTelefono = @idTelefono AND idDuenio = @idDuenio "
                };

                query.Parameters.Add("@numero_Telefono", MySqlDbType.VarChar, 10).Value = telefono.NumeroTelefono;
                query.Parameters.Add("@idTelefono", MySqlDbType.Int32, 11).Value = telefono.IdTelefono;
                query.Parameters.Add("@idDuenio", MySqlDbType.Int32, 11).Value = idPersona;

                query.ExecuteNonQuery();
                seActualizo = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Ocurrio un error al conectar a base de datos", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return seActualizo;
        }
    }
}
