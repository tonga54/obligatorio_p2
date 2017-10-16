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
            this.fechaRegistro = DateTime.Now;
            this.rol = "Organizador";
        }

        public override string ToString()
        {
            string resultado = "\n:::::::::::::::::::::::::::\n Rol: " + this.rol + "\n Email: " + this.Email + "\n Password: " + this.Password + "\n Nombre: " + this.nombre + "\n Telefono: " + this.telefono + "\n Direccion: " + this.direccion + "\n Fecha de registro: " + this.fechaRegistro + "\n:::::::::::::::::::::::::::\n";
            return resultado;
        }

        public bool verificarFecha(DateTime fecha)
        {
            for (int i = 0; i < this.eventos.Count; i++)
            {
                if (fecha == eventos[i].Fecha)
                {
                    return true;
                }
            }

            return false;
        }

        public void altaEvento(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes,int duracion)
        {
            Estandar ev = new Estandar(fecha, turno, descripcion, cliente, cantAsistentes, duracion);
            eventos.Add(ev);
        }

        public void altaEvento(DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes)
        {
            Premium ev = new Premium(fecha, turno, descripcion, cliente, cantAsistentes);
            eventos.Add(ev);
        }
        

    }
}

