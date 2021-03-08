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
    class DireccionDAO
    {
        private List<Direccion> direcciones;
        private Direccion direccion;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public DireccionDAO()
        {
            connection = new DataBaseConnection();
        }

        public List<Direccion> ConsultarDireccionesPorIDPersona(int idPersona)
        {
            try
            {
                direcciones = new List<Direccion>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT numero_Direccion, nombre_Direccion, ciudad, estado, pais FROM Direcciones WHERE idHabitante = @idHabitante "
                };

                MySqlParameter idHabitante = new MySqlParameter("@idHabitante", MySqlDbType.Int32, 11)
                {
                    Value = idPersona
                };

                query.Parameters.Add(idHabitante);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    direccion = new Direccion
                    {
                        NumeroDireccion = reader.GetInt32(0),
                        NombreDireccion = reader.GetString(1),
                        Ciudad = reader.GetString(2),
                        Estado = reader.GetString(3),
                        Pais = reader.GetString(4)
                    };

                    direcciones.Add(direccion);
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

            return direcciones;
        }


        public bool AltaDireccion(int idPersona, Direccion direccion)
        {
            bool seGuardo = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Direccion(numero_Direccion, nombre_Direccion, ciudad, estado, pais, idHabitante)" +
                    " VALUES (@numero_Direccion, @nombre_Direccion, @ciudad, @estado, @pais, @idHabitante) "
                };

                MySqlParameter numeroDireccion = new MySqlParameter("@numero_Direccion", MySqlDbType.Int32, 11)
                {
                    Value = direccion.NumeroDireccion

                };

                MySqlParameter nombreDireccion = new MySqlParameter("@nombre_Direccion", MySqlDbType.VarChar, 255)
                {
                    Value = direccion.NombreDireccion
                };

                MySqlParameter ciudad = new MySqlParameter("@ciudad", MySqlDbType.VarChar, 255)
                {
                    Value = direccion.Ciudad
                };

                MySqlParameter estado = new MySqlParameter("@estado", MySqlDbType.VarChar, 255)
                {
                    Value = direccion.Estado
                };

                MySqlParameter pais = new MySqlParameter("@pais", MySqlDbType.VarChar, 255)
                {
                    Value = direccion.Pais
                };


                MySqlParameter idHabitante = new MySqlParameter("@idHabitante", MySqlDbType.Int32, 11)
                {
                    Value = idPersona
                };

                query.Parameters.Add(numeroDireccion);
                query.Parameters.Add(nombreDireccion);
                query.Parameters.Add(ciudad);
                query.Parameters.Add(estado);
                query.Parameters.Add(pais);
                query.Parameters.Add(idHabitante);

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
        
        public bool ActualizarDireccion(int idPersona, Direccion direccionCambio)
        {
            bool seActualizo = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Direcciones SET numero_Direccion  = @numero_Direccion , " + 
                    "nombre_Direccion  = @nombre_Direccion , ciudad = @ciudad, estado = @estado, " +
                    "pais = @pais WHERE idDireccion = @idDireccion AND idHabitante = @idHabitante "
                };

                query.Parameters.Add("@numero_Direccion", MySqlDbType.Int32, 11).Value = direccionCambio.NumeroDireccion;
                query.Parameters.Add("@nombre_Direccion ", MySqlDbType.VarChar, 255).Value = direccionCambio.NombreDireccion;
                query.Parameters.Add("@ciudad", MySqlDbType.VarChar, 255).Value = direccionCambio.Ciudad;
                query.Parameters.Add("@estado", MySqlDbType.VarChar, 255).Value = direccionCambio.Estado;
                query.Parameters.Add("@pais", MySqlDbType.VarChar, 255).Value = direccionCambio.Pais;
                query.Parameters.Add("@idDireccion", MySqlDbType.Int32, 11).Value = direccionCambio.IdDireccion;
                query.Parameters.Add("@idHabitante", MySqlDbType.Int32, 11).Value = idPersona;

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