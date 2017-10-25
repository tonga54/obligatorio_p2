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

        public int Codigo
        {
            get
            {
                return this.codigo;
            }
        }

        public string Cliente
        {
            get
            {
                return this.cliente;
            }
        }


        public Evento(DateTime fecha, string turno, string descripcion, string cliente,int cantAsistentes, List<Servicio> servicio, List<int> cantPersonasServicio)
        {
            this.fecha = fecha.Date;
            this.turno = turno;
            this.descripcion = descripcion;
            this.cliente = cliente;
            this.cantAsistentes = cantAsistentes;
            this.codigo = Evento.ultCodigo;
            Evento.ultCodigo++;
            agregarServicio(servicio, cantPersonasServicio);
        }

        //Utilizado para listar el ultimo evento añadido
        public override string ToString()
        {
            string devolucion = "\n Codigo: " + this.codigo + "\n Cliente: " + this.cliente + "\n Turno: " + this.turno + "\n Descripcion: " + this.descripcion + "\n Cantidad Asistentes: " + this.cantAsistentes + "\n Costo total: $" + costoTotal() + "\n\n DETALLE SERVICIOS: \n\n";
            for(int i = 0; i < servicios.Count; i++)
            {
               devolucion += servicios[i].ToString();
            }
            return devolucion;
        }

        public void agregarServicio(List<Servicio> servicio,List<int> cantAsistentes)
        {
            for(int i = 0; i < servicio.Count; i++)
            {
                servicios.Add(new EventoServicio(servicio[i], cantAsistentes[i]));
            }
        }

        public abstract decimal costoTotal();
        
        
    }

}
