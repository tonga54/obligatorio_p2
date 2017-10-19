using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Empresa
    {
        private static List<Administrador> usuarios = new List<Administrador>();
        private List<Servicio> servicios = new List<Servicio>();

        public Empresa()
        {
            cargarDatosPrueba();
        }


        // --------------------------   INSERCION DE DATOS   --------------------------

        public string altaAdministrador(string email, string password)
        {
            if (verificarUsuario(email, password) != null)
            {
                return "Ya existe un usuario con ese email";
            }
            else
            {
                usuarios.Add(new Administrador(email, password));
                return "Administrador agregado con exito";
            }
        }

        public string altaOrganizador(string email, string password, string nombre, string telefono, string direccion)
        {
            if (verificarUsuario(email, password) != null)
            {
                return "Ya existe un usuario con ese email";
            }
            else
            {
                usuarios.Add(new Organizador(email, password, nombre, telefono, direccion));
                return "Organizador agregado con exito";
            }

        }

        //EVENTO ESTANDAR
        public string altaEvento(string email,string password, DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, int duracion, string nombreServicio, int cantPersonasServicio) {
            string devolucion = "";
            //busco al usuario solicitado
            Administrador adm = verificarUsuario(email, password);
            if (adm is Organizador && adm != null)
            {
                //si el usuario encontrado es diferente de nulo y es Organizador entonces lo casteo
                //una vez casteado verifico la fecha del evento, si esta disponible entonces
                //voy a buscar el servicio que el usuario introdujo (el nombre), si existe
                //procedo con crear el evento y guardarlo en la lista que posee el Organizador
                Organizador org = (Organizador)adm;
                if (verificarFechaEvento(fecha) == null)
                {
                    Servicio serv = buscarServicio(nombreServicio);
                    if (serv != null)
                    {
                        org.altaEvento(fecha, turno, descripcion, cliente, cantAsistentes, duracion, serv, cantPersonasServicio);
                    }
                    else
                    {
                        devolucion = "\nNo existe el servicio\n";
                    }
                }
                else
                {
                    devolucion = "\nYa hay un evento registrado para esa fecha\n";
                }
            }
            else
            {
                devolucion = "\nNo existe un usuario con ese mail y/o contraseña\n";
            }

            return devolucion;
        }

        //EVENTO PREMIUM
        public string altaEvento(string email, string password, DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, string nombreServicio,int cantPersonasServicio)
        {
            string devolucion = "";
            //busco al usuario solicitado
            Administrador adm = verificarUsuario(email, password);
            if (adm is Organizador && adm != null)
            {
                //si el usuario encontrado es diferente de nulo y es Organizador entonces lo casteo
                //una vez casteado verifico la fecha del evento, si esta disponible entonces
                //voy a buscar el servicio que el usuario introdujo (el nombre), si existe
                //procedo con crear el evento y guardarlo en la lista que posee el Organizador
                Organizador org = (Organizador)adm;
                if (verificarFechaEvento(fecha) == null)
                {
                    Servicio serv = buscarServicio(nombreServicio);
                    if(serv != null)
                    {
                        org.altaEvento(fecha, turno, descripcion, cliente, cantAsistentes, serv, cantPersonasServicio);
                    }
                    else
                    {
                        devolucion = "\nNo existe el servicio\n";
                    }
                }else
                {
                    devolucion = "\nYa hay un evento registrado para esa fecha\n";
                }
            }
            else
            {
                devolucion = "\nNo existe un usuario con ese mail y/o contraseña\n";
            }

            return devolucion;
            
        }



        // --------------------------   BUSQUEDA DE DATOS Y EXTRACCION   --------------------------


        private Evento verificarFechaEvento(DateTime fecha)
        {
            Evento ev = null;
            for (int i = 0; i < usuarios.Count; i++)
            {
                Administrador adm = usuarios[i];
                if (adm is Organizador)
                {
                    Organizador org = (Organizador)adm;
                    ev = org.verificarFecha(fecha);
                }
            }
            return ev;
        }

        public Servicio buscarServicio(string nombreServicio)
        {
            int i = 0;
            Servicio serv = null;
            while (i < servicios.Count && serv == null)
            {
                if (servicios[i].Nombre == nombreServicio)
                {
                    serv = servicios[i];
                }
                i++;
            }
            return serv;
        }

        public Administrador verificarUsuario(string email, string password)
        {
            int i = 0;
            Administrador usuario = null;
            while (i < Empresa.usuarios.Count && usuario == null)
            {
                if (Empresa.usuarios[i].Email == email && Empresa.usuarios[i].Password == password)
                {
                    usuario = Empresa.usuarios[i];
                }
                i++;
            }
            return usuario;
        }

        public static Administrador verificarUsuario(string email)
        {
            int i = 0;
            Administrador usuario = null;
            while (i < Empresa.usuarios.Count && usuario == null)
            {
                if (Empresa.usuarios[i].Email == email)
                {
                    usuario = Empresa.usuarios[i];
                }
                i++;
            }
            return usuario;
        }


        // --------------------------   LISTADO DE INFORMACION   --------------------------

        public string listarUsuarios()
        {
            string devolucion = "";
            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuarios[i] is Administrador)
                {
                    devolucion += usuarios[i];
                }
                else
                {
                    devolucion += usuarios[i];
                }
            }
            return devolucion;
        }

        public string listarEventos(string email)
        {
            string devolucion;
            Administrador user = verificarUsuario(email);
            if (user != null)
            {
                if (user is Organizador)
                {
                    Organizador org = (Organizador)user;
                    devolucion = org.listarEventos();
                }
                else
                {
                    devolucion = "El usuario ingresado no es un organizador";
                }
            }
            else
            {
                devolucion = "No existe el usuario";
            }

            return devolucion;
        }

        public string listarServicios()
        {
            string devolucion = "";
            for (int i = 0; i < servicios.Count; i++)
            {
                devolucion += servicios[i];
            }
            return devolucion;
        }



        // --------------------------   PRECARGA DE DATOS   --------------------------


        public void cargarDatosPrueba()
        {
            usuarios.Add(new Organizador("gaston@eventos2017.com", "password2", "Gaston2", "08321233", "dir2"));
            usuarios.Add(new Administrador("gaston@eventos2017.com", "password"));
            usuarios.Add(new Administrador("gaston@eventos2017.com", "password1"));
            usuarios.Add(new Organizador("gaston@eventos2017.com", "password1", "Gaston1", "08321233", "dir1"));
            usuarios.Add(new Administrador("gaston@eventos2017.com", "password2"));
            usuarios.Add(new Administrador("gaston@eventos2017.com", "password3"));
            usuarios.Add(new Organizador("gaston@eventos2017.com", "password3", "Gaston3", "08321233", "dir3"));

            servicios.Add(new Servicio("Cabalgata", "Cabalgata al atardecer", 50));
            servicios.Add(new Servicio("Paseo en barco", "Paseo por la costa", 250));
            servicios.Add(new Servicio("Fiesta en la playa", "Fiesta en la playa", 80));
            
            altaEvento("gaston@eventos2017.com", "password2", DateTime.Now, "Noche", "dasdsajhd", "Pedro", 20, "Cabalgata", 10);
            altaEvento("gaston@eventos2017.com", "password2", new DateTime(2017, 12, 24), "Tarde", "dasdsajhd", "Pedro", 10, 2, "Cabalgata", 5);
            altaEvento("gaston@eventos2017.com", "password2", new DateTime(2018, 01, 27), "Mañana", "dasdsajhd", "Pedro", 20, "Cabalgata", 10);
        }

        
    }

}
