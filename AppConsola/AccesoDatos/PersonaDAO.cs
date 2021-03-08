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
    public class PersonaDAO
    {
        private DireccionDAO direccionDAO;
        private TelefonoDAO telefonoDAO;
        private List<Persona> personas;
        private Persona persona;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public PersonaDAO()
        {
            connection = new DataBaseConnection();
        }

        public bool EliminarPersona(int idPersona)
        {
            bool seElimino = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "DELETE FROM Direcciones WHERE idHabitante=@IDPersona;"+
                    "DELETE FROM Telefonos WHERE idDuenio=@IDPersona;"+
                    "DELETE FROM Personas WHERE idPersona=@IDPersona "
                };

                MySqlParameter IDPersona = new MySqlParameter("@IDPersona", MySqlDbType.Int32, 11)
                {
                    Value = idPersona
                };

                query.Parameters.Add(IDPersona);

                query.ExecuteNonQuery();
                seElimino = true;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Ocurrio un error al conectar a base de datos", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return seElimino;
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
                    CommandText = "SELECT * FROM Personas "
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

                    personas.Add(persona);
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

        public Persona ConsultarPersonaPorID(int idPersona)
        {
            try
            {
                direccionDAO = new DireccionDAO();
                telefonoDAO = new TelefonoDAO();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Personas WHERE idPersona=@idPersona"
                };
                MySqlParameter IDPersona = new MySqlParameter("@idPersona", MySqlDbType.Int32, 11)
                {
                    Value = idPersona
                };

                query.Parameters.Add(IDPersona);

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

            return persona;
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
                    " VALUES (@nombres, @apellidos, @genero, @edad) "
                };

                MySqlParameter nombres = new MySqlParameter("@nombres", MySqlDbType.VarChar, 255)
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

                MySqlParameter edad = new MySqlParameter("@edad", MySqlDbType.Int32, 2)
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

        public bool ActualizarPersona(Persona personaCambio)
        {
            bool seActualizo = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Personas SET nombres = @nombres, " + 
                    "apellidos = @apellidos, genero = @genero, edad = @edad WHERE idPersona = @idPersona "
                };

                query.Parameters.Add("@nombres", MySqlDbType.VarChar, 255).Value = personaCambio.Nombres;
                query.Parameters.Add("@apellidos", MySqlDbType.VarChar, 255).Value = personaCambio.Apellidos;
                query.Parameters.Add("@genero", MySqlDbType.VarChar, 255).Value = personaCambio.Genero;
                query.Parameters.Add("@edad", MySqlDbType.Int32, 2).Value = personaCambio.Edad;
                query.Parameters.Add("@idPersona", MySqlDbType.Int32, 11).Value = personaCambio.IdPersona;

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
