using System;

namespace MyConsoleApp
{
    class Program
    {
        private int comando = 0;
        static void Main(string[] args)
        {
            do
            {
                int comando = 0;
                Console.WriteLine("Gestion de base de datos Personas");
                Console.WriteLine("Ingrese un numero: ");
                Console.WriteLine("1) Dar Alta Persona ");
                Console.WriteLine("2) Actualizar Persona ");
                Console.WriteLine("3) Eliminar Persona ");
                Console.WriteLine("4) Consultar Persona ");
                Console.WriteLine("5) Salir ");
                comando = Console.Read();

                switch (comando)
                {
                    case 1: Console.WriteLine("CREACION DE PERSONA ");
                        Console.WriteLine("Ingrese los nombres");
                        nuevoNombres = Console.ReadLine();
                        Console.WriteLine("Ingrese los apellidos");
                        nuevoApellidos = Console.ReadLine();
                        Console.WriteLine("Ingrese el genero");
                        nuevoGenero = Console.ReadLine();
                        Console.WriteLine("Ingrese la edad");
                        nuevoEdad = Console.ReadLine();
                        Persona nuevaPersona = new Persona
                        {
                            Nombres = nuevoNombres,
                            Apellidos = nuevoApellidos,
                            Genero = nuevoGenero,
                            Edad = nuevoEdad
                        };


                        PersonaDAO personaDAO = new PersonaDAO();
                        bool seGuardo = personaDAO.AltaPersona(nuevaPersona);

                        if (seGuardo)
                        {
                            Console.WriteLine("Ingrese el numero de direcciones");
                        }
                            
                        break;
                    case 2:
                        Console.WriteLine("ACTUALIZACION DE PERSONA ");
                        break;
                    case 3:
                        Console.WriteLine("ELIMINACION DE PERSONA ");
                        break;
                    case 4:
                        Console.WriteLine("CONSULTA DE PERSONA ");
                        break;
                    default:
                        Console.WriteLine("ERROR. Ingrese un numero entre 1 y 5 ");
                        break;

                }

            } while (comando != 5);
            Console.WriteLine("SALIENDO... ");
            Environment.Exit(0);
        }
    }
}
