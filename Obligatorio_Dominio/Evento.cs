using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
   abstract class Evento
    {
        protected DateTime fecha;
        protected string turno;
        protected string descripcion;
        protected string cliente;
        protected int cantAsistentes;
        protected int codigo;
        protected static int ultCodigo;

        public DateTime Fecha
        {
            get
            {
                return this.fecha;
            }
        }

        public Evento(DateTime fecha, string turno, string descripcion, string cliente,int cantAsistentes)
        {
            this.fecha = fecha;
            this.turno = turno;
            this.descripcion = descripcion;
            this.cliente = cliente;
            this.cantAsistentes = cantAsistentes;
            this.codigo = Evento.ultCodigo;
            Evento.ultCodigo++;
        }

    }

}
