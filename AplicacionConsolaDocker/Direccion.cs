/*
    Date: 01/03/2021
    Author(s) : Ricardo Moguel Sánchez
 */
using System;

namespace MyConsoleApp
{
    public class Direccion
    {
        private int idDireccion;
        private int numeroDireccion;
        private string nombreDireccion;
        private string ciudad;
        private string estado;
        private string pais;

        public Direccion() { }

        public int IdDireccion
        {
            get => idDireccion;
            set => idDireccion = value;
        }

        public int NumeroDireccion
        {
            get => numeroDireccion;
            set => numeroDireccion = value;
        }

        public string NombreDireccion
        {
            get => nombreDireccion;
            set => nombreDireccion = value;
        }

        public string Ciudad
        {
            get => ciudad;
            set => nombreDireccion = value;
        }

        public string Estado
        {
            get => estado;
            set => estado = value;
        }

        public string Pais
        {
            get => pais;
            set => pais = value;
        }
    }
}
