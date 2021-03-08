using System;
using Dominio;
using AccesoDatos;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace AppConsola
{
    public class Program
    {


        public static void Main(string[] args)
        {
            int comando = 0;
            DireccionDAO direccionDAO = null;
            Direccion nuevaDireccion = null;
            TelefonoDAO telefonoDAO = null;
            Telefono nuevoTelefono = null;
            PersonaDAO personaDAO = null;
            Persona nuevaPersona = null;
            List<Persona> personas = null;
            Persona personaSeleccionada = null;
            Direccion direccionSeleccionada = null;
            Telefono telefonoSeleccionado = null;
            bool seGuardo = false;

            do
            {
                if(comando != 5)
                {
                    comando = 0;
                    Console.WriteLine("Gestion de base de datos Personas");
                    Console.WriteLine("Ingrese un numero: ");
                    Console.WriteLine("1) Dar Alta Persona ");
                    Console.WriteLine("2) Consultar Persona ");
                    Console.WriteLine("3) Actualizar Persona ");
                    Console.WriteLine("4) Eliminar Persona ");
                    Console.WriteLine("5) Salir ");
                    comando = Convert.ToInt32(Console.ReadLine());

                    switch (comando)
                    {
                        case 1: 
                            Console.WriteLine("CREACION DE PERSONA");
                            Console.WriteLine("Ingrese los nombres");
                            nuevaPersona.Nombres = Console.ReadLine();
                            Console.WriteLine("Ingrese los apellidos");
                            nuevaPersona.Apellidos = Console.ReadLine();
                            Console.WriteLine("Ingrese el genero");
                            nuevaPersona.Genero = Console.ReadLine();
                            Console.WriteLine("Ingrese la edad");
                            nuevaPersona.Edad = Convert.ToInt32(Console.ReadLine());

                            personaDAO = new PersonaDAO();
                            seGuardo = personaDAO.AltaPersona(nuevaPersona);

                            if (seGuardo)
                            {
                                Console.WriteLine("Ingrese el numero de direcciones");
                                comando = Convert.ToInt32(Console.ReadLine());

                                direccionDAO = new DireccionDAO();
                                telefonoDAO = new TelefonoDAO();
                                seGuardo = false;
                                nuevaDireccion = new Direccion();
                                nuevoTelefono = new Telefono();

                                for (int i = 0; i < comando; i++)
                                {
                                    Console.WriteLine("Ingrese el numero de direccion para la Direccion "+i);
                                    nuevaDireccion.NumeroDireccion = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine("Ingrese los nombres de direccion para la Direccion "+i);
                                    nuevaDireccion.NombreDireccion = Console.ReadLine();
                                    Console.WriteLine("Ingrese la ciudad para la Direccion "+i);
                                    nuevaDireccion.Ciudad = Console.ReadLine();
                                    Console.WriteLine("Ingrese el estado de la ciudad de la Direccion "+i);
                                    nuevaDireccion.Estado = Console.ReadLine();
                                    Console.WriteLine("Ingrese el pais del estado de la direccion "+i);
                                    nuevaDireccion.Pais = Console.ReadLine();
                                    nuevaDireccion.IdHabitante = nuevaPersona.IdPersona;

                                    seGuardo = direccionDAO.AltaDireccion(nuevaPersona.IdPersona, nuevaDireccion);
                                    if(seGuardo == false){
                                        Console.WriteLine("Error. No se logro guardar la Direccion "+i);
                                    } 
                                    else
                                    {
                                        nuevaPersona.Direcciones.Add(nuevaDireccion);
                                    }
                                }

                                Console.WriteLine("Ingrese el numero de telefonos");
                                comando = Convert.ToInt32(Console.ReadLine());
                                seGuardo = false;

                                for (int i = 0; i < comando; i++)
                                {
                                    Console.WriteLine("Ingrese el numero de telefono para Telefono "+i);
                                    nuevoTelefono.NumeroTelefono = Console.ReadLine();
                                    nuevoTelefono.IdDuenio = nuevaPersona.IdPersona;

                                    seGuardo = telefonoDAO.AltaTelefono(nuevaPersona.IdPersona, nuevoTelefono);
                                    if(seGuardo == false){
                                        Console.WriteLine("Error. No se logro guardar el Telefono "+i);
                                    } 
                                    else
                                    {
                                        nuevaPersona.Telefonos.Add(nuevoTelefono);
                                    }
                                }
                            }
                                
                            break;
                        case 2:
                            Console.WriteLine("CONSULTA DE PERSONAS ");
                            personaDAO = new PersonaDAO();
                            personas = personaDAO.ConsultarPersonas();
                            foreach (Persona personaConsultada in personas)
                            {
                                Console.WriteLine("\nPersona: ");
                                Console.WriteLine("ID Persona:"+ personaConsultada.IdPersona);
                                Console.WriteLine("Nombres :"+ personaConsultada.Nombres);
                                Console.WriteLine("Apellidos:"+ personaConsultada.Apellidos);
                                Console.WriteLine("Genero:"+ personaConsultada.Genero);
                                Console.WriteLine("Edad:"+ personaConsultada.Edad);
                                Console.WriteLine("Direcciones:");
                                
                                foreach (Direccion direccionConsultada in personaConsultada.Direcciones)
                                {
                                    Console.WriteLine("\nID Direccion:"+ direccionConsultada.IdDireccion);
                                    Console.WriteLine("Numero Direccion :"+ direccionConsultada.NumeroDireccion);
                                    Console.WriteLine("Nombre Direccion:"+ direccionConsultada.NombreDireccion);
                                    Console.WriteLine("Ciudad:"+ direccionConsultada.Ciudad);
                                    Console.WriteLine("Estado:"+ direccionConsultada.Estado);
                                    Console.WriteLine("Pais:"+ direccionConsultada.Pais);
                                }

                                foreach (Telefono telefonoConsultado in personaConsultada.Telefonos)
                                {
                                    Console.WriteLine("\nID Direccion:"+ telefonoConsultado.IdTelefono);
                                    Console.WriteLine("Numero Telefono :"+ telefonoConsultado.NumeroTelefono);
                                }
                            }

                            break;
                        case 3: Console.WriteLine("ACTUALIZACION DE PERSONA ");
                            Console.WriteLine("Ingrese la ID de la Persona a actualizar");
                            int idSeleccion = Convert.ToInt32(Console.ReadLine());
                            
                            try
                            {
                                personaSeleccionada = personaDAO.ConsultarPersonaPorID(idSeleccion);
                            }
                            catch (MySqlException ex)
                            {
                                Console.WriteLine("Ocurrio un error al conectar a base de datos", ex);
                            }

                            if(personaSeleccionada != null)
                            {
                                Console.WriteLine("Nombre: "+personaSeleccionada.Nombres+" \nIngrese los nuevos nombres");
                                personaSeleccionada.Nombres = Console.ReadLine();
                                Console.WriteLine("Apellidos: "+personaSeleccionada.Apellidos+" \nIngrese los nuevos apellidos");
                                personaSeleccionada.Apellidos = Console.ReadLine();
                                Console.WriteLine("Genero: "+personaSeleccionada.Genero+" \nIngrese el nuevo genero");
                                personaSeleccionada.Genero = Console.ReadLine();
                                Console.WriteLine("Edad: "+personaSeleccionada.Edad+" \nIngrese la nueva edad");
                                personaSeleccionada.Edad = Convert.ToInt32(Console.ReadLine());

                                personaDAO = new PersonaDAO();
                                seGuardo = personaDAO.ActualizarPersona(personaSeleccionada);

                                if (seGuardo)
                                {
                                    direccionDAO = new DireccionDAO();
                                    telefonoDAO = new TelefonoDAO();
                                    seGuardo = false;

                                    for (int i = 0; i < personaSeleccionada.Direcciones.Count; i++)
                                    {
                                        Console.WriteLine("Numero: "+direccionSeleccionada.NumeroDireccion+" \nIngrese el nuevo numero de direccion para la Direccion "+i);
                                        direccionSeleccionada.NumeroDireccion = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Nombre: "+direccionSeleccionada.NombreDireccion+" \nIngrese los nuevos nombres de direccion para la Direccion "+i);
                                        direccionSeleccionada.NombreDireccion = Console.ReadLine();
                                        Console.WriteLine("Ciudad: "+direccionSeleccionada.Ciudad+" \nIngrese la nueva ciudad para la Direccion "+i);
                                        direccionSeleccionada.Ciudad = Console.ReadLine();
                                        Console.WriteLine("Estado: "+direccionSeleccionada.Estado+" \nIngrese el nuevo estado de la ciudad de la Direccion "+i);
                                        direccionSeleccionada.Estado = Console.ReadLine();
                                        Console.WriteLine("Pais: "+direccionSeleccionada.Pais+" \nIngrese el nuevo pais del estado de la direccion "+i);
                                        direccionSeleccionada.Pais = Console.ReadLine();

                                        seGuardo = direccionDAO.ActualizarDireccion(personaSeleccionada.IdPersona, direccionSeleccionada);
                                        if(seGuardo == false)
                                        {
                                            Console.WriteLine("Error. No se logro guardar la Direccion "+i);
                                        } 
                                    }

                                    for (int i = 0; i < personaSeleccionada.Telefonos.Count; i++)
                                    {
                                        Console.WriteLine("Numero Tel: "+telefonoSeleccionado.NumeroTelefono+" \nIngrese el nuevo numero de telefono para Telefono "+i);
                                        telefonoSeleccionado.NumeroTelefono = Console.ReadLine();

                                        seGuardo = telefonoDAO.ActualizarTelefono(personaSeleccionada.IdPersona, telefonoSeleccionado);
                                        if(seGuardo == false)
                                        {
                                            Console.WriteLine("Error. No se logro guardar el Telefono "+i);
                                        } 
                                    }
                                }
                            }

                            break;
                        case 4:Console.WriteLine("ELIMINACION DE PERSONA ");
                            Console.WriteLine("Ingrese la ID de la Persona a eliminar");
                            idSeleccion = Convert.ToInt32(Console.ReadLine());
                            seGuardo = false;
                            try
                            {
                                seGuardo = personaDAO.EliminarPersona(idSeleccion);
                            }
                            catch (MySqlException ex)
                            {
                                Console.WriteLine("Ocurrio un error al conectar a base de datos", ex);
                            }

                            if(seGuardo == true)
                            {
                                Console.WriteLine("Se elimino con exito la persona con ID "+idSeleccion);
                            }
                            else
                            {
                                Console.WriteLine("Ocurrio un error y no se logro eliminar a la persona seleccionada");
                            }
                            break;
                            case 5: 
                                break;
                        default:
                            Console.WriteLine("ERROR. Ingrese un numero entre 1 y 5 ");
                            break;

                    }
                }
                
            } while (comando != 5);
            Console.WriteLine("SALIENDO... ");
            Environment.Exit(0);
        }
    }
}