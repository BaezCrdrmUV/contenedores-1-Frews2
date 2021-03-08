/*
    Date: 01/03/2021
    Author(s) : Ricardo Moguel Sánchez
 */
using System;

namespace Dominio
{
    public class Telefono
    {
        private int idTelefono;
        private string numeroTelefono;
        private int idDuenio;

        public Telefono() { }
        public int IdTelefono
        {
            get => idTelefono;
            set => idTelefono = value;
        }

        public string NumeroTelefono
        {
            get => numeroTelefono;
            set => numeroTelefono = value;
        }

        public int IdDuenio
        {
            get => idDuenio;
            set => idDuenio = value;
        }
    }
}
