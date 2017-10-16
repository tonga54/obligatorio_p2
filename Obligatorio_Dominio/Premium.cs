using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    class Premium : Evento
    {
        private static double porcentajeAumento = 5.5;

        public Premium(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes) : base(fecha, turno, descripcion, cliente, cantAsistentes)
        {
            this.fecha = fecha;
        }
    }
}
