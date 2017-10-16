using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    class Servicio
    {
        private string nombre;
        private string descripcion;
        private decimal precio;

        public Servicio(string nombre, string descripcion, decimal precio)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
        }


    }
    
}
