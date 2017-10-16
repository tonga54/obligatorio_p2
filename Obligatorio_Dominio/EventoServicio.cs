using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    class EventoServicio
    {
        private Servicio servicio;
        private int cantAsistentes;

        public EventoServicio(Servicio servicio, int cantAsistentes)
        {
            this.servicio = servicio;
            this.cantAsistentes = cantAsistentes;
        }
    }
}
