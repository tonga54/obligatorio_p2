using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    class Premium : Evento
    {
        private static decimal aumento = 1.05m;

        public Premium(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, List<Servicio> serv, List<int> cantPersonasServicio) : base(fecha, turno, descripcion, cliente, cantAsistentes,serv, cantPersonasServicio)
        {
            this.fecha = fecha;
        }

        public override decimal costoTotal()
        {
            decimal total = 0;
            for(int i = 0; i < servicios.Count; i++)
            {
                total += servicios[i].calcularTotal();
            }

            return total * Premium.aumento;
        }
    }
}
