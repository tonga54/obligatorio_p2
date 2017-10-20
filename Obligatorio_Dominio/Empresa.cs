using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Empresa
    {
        private List<Administrador> usuarios = new List<Administrador>();
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
        public string altaEvento(string email,string password, DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, int duracion, List<string> servicio, List<int> cantPersonasServicio) {
            string devolucion = "";
            //busco al usuario solicitado
            Administrador adm = verificarUsuario(email, password);
            if (adm is Organizador && adm != null)
            {
                //si el usuario encontrado es diferente de nulo y es Organizador entonces lo casteo
                //una vez casteado verifico la fecha del evento esta disponible
                Organizador org = (Organizador)adm;
                if (verificarFechaEvento(fecha) == null)
                {

                    /*Con la lista de strings con los nombres de los servicios que recibo, recorro dicha lista
                    por cada uno de los elementos voy al metodo buscarServicio el cual se encarga de verificar si existe
                    dicho servicio, si existe entonces mi bandera permanece en false y añado ese elemento a la lista de Servicios
                    'servicioEvento', si la bandera no cambia entonces quiere decir que todos los servicios que escribio el usuario existen 
                    de lo contrario la bandera cambiara a true y no podre guardar el evento.*/

                    List<Servicio> servicioEvento = new List<Servicio>();
                    int i = 0;
                    bool bandera = false;

                    while (i < servicio.Count && bandera == false)
                    {
                        Servicio serv = buscarServicio(servicio[i]);
                        if (serv == null)
                        {
                            bandera = true;
                        }else
                        {
                            servicioEvento.Add(serv);
                        }
                        i++;
                    }

                    if (!bandera)
                    {
                        org.altaEvento(fecha, turno, descripcion, cliente, cantAsistentes, duracion, servicioEvento, cantPersonasServicio);
                        Console.WriteLine(org.ultimoEvento());
                        
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
        public string altaEvento(string email, string password, DateTime fecha, string turno, string descripcion, string cliente, int cantAsistentes, List<string> servicio,List<int>cantPersonasServicio)
        {
            string devolucion = "";
            Administrador adm = verificarUsuario(email, password);
            if (adm is Organizador && adm != null)
            {
                Organizador org = (Organizador)adm;
                if (verificarFechaEvento(fecha) == null)
                {
                    List<Servicio> servicioEvento = new List<Servicio>();
                    int i = 0;
                    bool bandera = false;

                    while (i < servicio.Count && bandera == false)
                    {
                        Servicio serv = buscarServicio(servicio[i]);
                        if (serv == null)
                        {
                            bandera = true;
                        }
                        else
                        {
                            servicioEvento.Add(serv);
                        }
                        i++;
                    }

                    if (!bandera)
                    {
                        org.altaEvento(fecha, turno, descripcion, cliente, cantAsistentes, servicioEvento, cantPersonasServicio);
                        Console.WriteLine(org.ultimoEvento());
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
            int i = 0;
            while (i < usuarios.Count && ev == null)
            {
                Administrador adm = usuarios[i];
                if (adm is Organizador)
                {
                    Organizador org = (Organizador)adm;
                    ev = org.verificarFecha(fecha.Date);
                }
                i++;
            }
            return ev;
        }

        private Servicio buscarServicio(string nombreServicio)
        {
            int i = 0;
            Servicio serv = null;
            while (i < servicios.Count && serv == null)
            {
                if (servicios[i].Nombre.ToLower() == nombreServicio.ToLower())
                {
                    serv = servicios[i];
                }
                i++;
            }
            return serv;
        }

        private Administrador verificarUsuario(string email, string password)
        {
            int i = 0;
            Administrador usuario = null;
            while (i < usuarios.Count && usuario == null)
            {
                if (usuarios[i].Email == email && usuarios[i].Password == password)
                {
                    usuario = usuarios[i];
                }
                i++;
            }
            return usuario;
        }

        private Administrador verificarUsuario(string email)
        {
            int i = 0;
            Administrador usuario = null;
            while (i < usuarios.Count && usuario == null)
            {
                if (usuarios[i].Email == email)
                {
                    usuario = usuarios[i];
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
            usuarios.Add(new Organizador("liliana@eventos2017.com", "password3", "Liliana", "08321233", "dir3"));

            servicios.Add(new Servicio("Cabalgata", "Cabalgata al atardecer", 50));
            servicios.Add(new Servicio("Paseo en barco", "Paseo por la costa", 250));
            servicios.Add(new Servicio("Fiesta en la playa", "Fiesta en la playa", 80));

            List<string> serviciosPrueba = new List<string>();
            List<int> cantAsistentesPrueba = new List<int>();

            List<string> serviciosPruebaDos = new List<string>();
            List<int> cantAsistentesPruebaDos = new List<int>();

            serviciosPrueba.Add("Cabalgata");
            serviciosPrueba.Add("Paseo en barco");

            cantAsistentesPrueba.Add(5);
            cantAsistentesPrueba.Add(6);


            serviciosPruebaDos.Add("Fiesta en la playa");
            cantAsistentesPruebaDos.Add(9);

            altaEvento("gaston@eventos2017.com", "password2", DateTime.Now, "Noche", "dasdsajhd", "Pedro", 20, 4, serviciosPrueba, cantAsistentesPrueba);
            altaEvento("gaston@eventos2017.com", "password2", new DateTime(2017, 12, 24), "Tarde", "dasdsajhd", "Pedro", 10, 2, serviciosPrueba, cantAsistentesPrueba);
            altaEvento("gaston@eventos2017.com", "password2", new DateTime(2018, 01, 27), "Mañana", "dasdsajhd", "Pedro", 20, 3, serviciosPruebaDos, cantAsistentesPruebaDos);
        }

        
    }

}
