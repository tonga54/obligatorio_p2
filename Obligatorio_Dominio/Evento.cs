using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
   public abstract class Evento
    {
        protected DateTime fecha;
        protected string turno;
        protected string descripcion;
        protected string cliente;
        protected int cantAsistentes;
        protected int codigo;
        protected static int ultCodigo;
        protected List<EventoServicio> servicios = new List<EventoServicio>();

        public DateTime Fecha
        {
            get
            {
                return this.fecha;
            }
        }

        public Evento(DateTime fecha, string turno, string descripcion, string cliente,int cantAsistentes, Servicio servicio, int cantPersonasServicio)
        {
            this.fecha = fecha;
            this.turno = turno;
            this.descripcion = descripcion;
            this.cliente = cliente;
            this.cantAsistentes = cantAsistentes;
            this.codigo = Evento.ultCodigo;
            Evento.ultCodigo++;
            agregarServicio(servicio, cantAsistentes);
        }

        public override string ToString()
        {
            string devolucion = " Codigo " + this.codigo + "\n Cliente: " + this.cliente;
            return devolucion;
        }

        private void agregarServicio(Servicio servicio,int cantAsistentes)
        {
            servicios.Add(new EventoServicio(servicio, cantAsistentes));
        }

    }

}
