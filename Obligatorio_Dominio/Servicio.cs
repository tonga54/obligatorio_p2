﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Servicio
    {
        private string nombre;
        private string descripcion;
        private decimal precio;

        public string Nombre
        {
            get
            {
                return this.nombre;
            }
        }

        public decimal Precio
        {
            get
            {
                return this.precio;
            }

            set
            {
                this.precio = value;
            }

        }

        public Servicio(string nombre, string descripcion, decimal precio)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
        }

        // Utilizado para mostrar el resumen una vez que se añade un evento.
        public override string ToString()
        {
            string devolucion = "\n Nombre: " + this.nombre + "\n Descripcion: " + this.descripcion + "\n Precio: $" + this.precio + "\n";
            return devolucion;
        }
 
    }
    
}
