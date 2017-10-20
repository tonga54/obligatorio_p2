using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class EventoServicio
    {
        private Servicio servicio;
        private int cantAsistentes; // del servicio no del evento
        private decimal precioTotal;

        public EventoServicio(Servicio servicio, int cantAsistentes)
        {
            this.servicio = servicio;
            this.cantAsistentes = cantAsistentes;
            this.precioTotal = calcularTotal();
        }

        public decimal calcularTotal()
        {
            return this.servicio.Precio * cantAsistentes;
        }

        //Utilizado para listar el ultimo evento añadido
        public override string ToString()
        {
            string devolucion = " Contratado para " + this.cantAsistentes + " personas";
            devolucion += servicio.ToString();
            devolucion += " Costo servicio: $" + calcularTotal() + "\n\n";
            return devolucion;
        }
        
    }
}
