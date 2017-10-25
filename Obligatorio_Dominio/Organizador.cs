using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Organizador : Administrador
    {
        private string nombre;
        private string direccion;
        private string telefono;
        private DateTime fechaRegistro;
        private List<Evento> eventos = new List<Evento>();

        public Organizador(string email, string password, string nombre, string telefono, string direccion) : base(email, password)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.fechaRegistro = DateTime.Now.Date;
            this.rol = "Organizador";
        }

        public override string ToString()
        {
            string resultado = "\n:::::::::::::::::::::::::::\n Rol: " + this.rol + "\n Email: " + this.Email + "\n Password: " + this.Password + "\n Nombre: " + this.nombre + "\n Telefono: " + this.telefono + "\n Direccion: " + this.direccion + "\n Fecha de registro: " + this.fechaRegistro + "\n:::::::::::::::::::::::::::\n";
            return resultado;
        }

        public Evento verificarFecha(DateTime fecha)
        {
            int i = 0;
            Evento ev = null;
            while(i < eventos.Count && ev == null)
            {
                if (fecha == eventos[i].Fecha.Date)
                {
                    ev = eventos[i];
                }
                i++;
            }

            return ev;
        }

        //public Evento buscarEvento () PARA AGREGARLE SERVICIOS EN EL FUTURO


        //EVENTO ESTANDAR
        public void altaEvento(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes,int duracion, List<Servicio> serv, List<int> cantPersonasServicio)
        {
            Estandar ev = new Estandar(fecha, turno, descripcion, cliente, cantAsistentes, duracion, serv, cantPersonasServicio);
            eventos.Add(ev);
        }

        //EVENTO PREMIUM
        public void altaEvento(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, List<Servicio> serv, List<int> cantPersonasServicio)
        {
            Premium ev = new Premium(fecha, turno, descripcion, cliente, cantAsistentes, serv, cantPersonasServicio);
            eventos.Add(ev);
        }

        //parte agregada

        public void buscarEventoYAgregarServicio(Evento ev, List<Servicio> servicio, List<int> cantAsistentes)
        {
            int i = 0;
            bool bandera = false;
            while(i < eventos.Count && bandera == false)
            {
                if (eventos[i].Equals(ev))
                {
                    eventos[i].agregarServicio(servicio,cantAsistentes);
                }
                i++;
            }
        }

        public string listarEventos()
        {
            string devolucion = "";
            if(eventos.Count == 0)
            {
                devolucion = "\n Este organizador no posee eventos \n";
            }else
            {
                decimal costoTotal = 0;
                for (int i = 0; i < eventos.Count; i++)
                {
                    devolucion += "\n Nombre: " + this.nombre;
                    devolucion += "\n Codigo: " + eventos[i].Codigo + "\n";
                    devolucion += " Cliente: " + eventos[i].Cliente + "\n";
                    devolucion += " Costo: $" + eventos[i].costoTotal() + "\n";
                    costoTotal += eventos[i].costoTotal();
                }
                devolucion += "\n    TOTAL : $" + costoTotal;
            }
            
            return devolucion;
        }

        public string ultimoEvento()
        {
            string devolucion = "\n          RESUMEN EVENTO          \n";
            Evento ultimoEvento = eventos[eventos.Count - 1];
            devolucion += ultimoEvento.ToString();
            return devolucion;
        }

    }
}

