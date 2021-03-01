/*
    Date: 01/03/2021
    Author(s) : Ricardo Moguel Sánchez
 */
using System;
using System.Collections.Generic;

namespace MyConsoleApp
{
    public class Persona
    {
        private int idPersona;
        private string nombres;
        private string apellidos;
        private string genero;
        private int edad;
        private List<Telefono> telefonos;
        private List<Direccion> direcciones;

        public Persona() { }
        public int IdPersona
        {
            get => idPersona;
            set => idPersona = value;
        }

        public string Nombres
        {
            get => nombres;
            set => nombres = value;
        }

        public string Apellidos
        {
            get => apellidos;
            set => apellidos = value;
        }

        public string Genero
        {
            get => genero;
            set => genero = value;
        }

        public int Edad
        {
            get => edad;
            set => edad = value;
        }

        public List<Telefono> Telefonos
        {
            get => telefonos;
            set => telefonos = value;
        }

        public List<Direccion> Direcciones
        {
            get => direcciones;
            set => direcciones = value;
        }
    }
}
