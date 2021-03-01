/*
    Date: 28/02/2021
    Author(s) : Ricardo Moguel Sánchez
 */
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace MyConsoleApp
{
    public class PersonaDAO
    {
        private DireccionDAO direccionDAO;
        private TelefonoDAO telefonoDAO;
        private List<Persona> personas;
        private Persona persona;
        private DataBaseConnection connection;
        private List<Direccion> direcciones;
        private Direccion direccion;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public PersonaDAO()
        {
            connection = new DataBaseConnection();
        }

        public List<Persona> ConsultarPersonas()
        {
            try
            {
                direccionDAO = new DireccionDAO();
                telefonoDAO = new TelefonoDAO();
                personas = new List<Persona>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Personas"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    persona = new Persona
                    {
                        IdPersona = reader.GetInt32(0),
                        Nombres = reader.GetString(1),
                        Apellidos = reader.GetString(2),
                        Genero = reader.GetString(3),
                        Edad = reader.GetInt32(4),
                        Direcciones = direccionDAO.ConsultarDireccionesPorIDPersona(reader.GetInt32(0)),
                        Telefonos = telefonoDAO.ConsultarTelefonosPorIDPersona(reader.GetInt32(0))
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

            return personas;
        }


        public bool AltaPersona(Persona persona)
        {
            bool seGuardo = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Persona(nombres, apellidos, genero, edad)" +
                    " VALUES (@nombres, @apellidos, @genero, @edad)"
                };

                MySqlParameter nombres = new MySqlParameter("@nombres", MySqlDbType.Int32, 11)
                {
                    Value = persona.Nombres

                };

                MySqlParameter apellidos = new MySqlParameter("@apellidos", MySqlDbType.VarChar, 255)
                {
                    Value = persona.Apellidos
                };

                MySqlParameter genero = new MySqlParameter("@genero", MySqlDbType.VarChar, 255)
                {
                    Value = persona.Genero
                };

                MySqlParameter edad = new MySqlParameter("@edad", MySqlDbType.VarChar, 255)
                {
                    Value = persona.Edad
                };

                query.Parameters.Add(nombres);
                query.Parameters.Add(apellidos);
                query.Parameters.Add(genero);
                query.Parameters.Add(edad);

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

    }
}
