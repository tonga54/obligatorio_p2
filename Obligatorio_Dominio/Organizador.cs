using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    class Organizador : Administrador
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
                if (fecha.Date == eventos[i].Fecha)
                {
                    ev = eventos[i];
                }
                i++;
            }

            return ev;
        }

        //public Evento buscarEvento () PARA AGREGARLE SERVICIOS EN EL FUTURO


        //EVENTO ESTANDAR
        public void altaEvento(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes,int duracion, Servicio serv, int cantPersonasServicio)
        {
            Estandar ev = new Estandar(fecha, turno, descripcion, cliente, cantAsistentes, duracion, serv, cantPersonasServicio);
            eventos.Add(ev);
        }

        //EVENTO PREMIUM
        public void altaEvento(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, Servicio serv, int cantPersonasServicio)
        {
            Premium ev = new Premium(fecha, turno, descripcion, cliente, cantAsistentes, serv, cantPersonasServicio);
            eventos.Add(ev);
        }

        public string listarEventos()
        {
            string devolucion = "";
            decimal costoTotal = 0;
            for(int i = 0; i < eventos.Count; i++)
            {
                devolucion += "\n:::::::::::::::::::::::::::\n";
                devolucion += "\n Nombre: " + this.nombre;
                devolucion += "\n Codigo: " + eventos[i].Codigo + "\n";
                devolucion += " Cliente: " + eventos[i].Cliente + "\n";
                devolucion += " Costo: $" + eventos[i].costoTotal() + "\n";
                costoTotal += eventos[i].costoTotal();
                devolucion += "\n:::::::::::::::::::::::::::\n";
            }
            devolucion += "\n\n:::::::::::::::::::::::::::::::::::::::::::::  COSTO TOTAL: $" + costoTotal;
            return devolucion;
        }

    }
}

