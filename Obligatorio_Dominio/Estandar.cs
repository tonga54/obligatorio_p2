using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    class Estandar : Evento
    {
        private static decimal limpieza = 500;
        private int duracion;

        public Estandar(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, int duracion, Servicio serv, int cantPersonasServicio) : base(fecha, turno, descripcion, cliente, cantAsistentes, serv, cantPersonasServicio)
        {
            this.duracion = duracion;
        }



    }

}