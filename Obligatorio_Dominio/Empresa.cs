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


        public static Administrador verificarUsuario(string email, string password)
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



       /* public bool altaAdministrador(string email, string password)
        {
            if (verificarUsuario(email, password) != null)
            {
                return false;
            } else
            {
                usuarios.Add(new Administrador(email, password));
                return true;
            }
        }*/


        public bool altaAdministrador(string email, string password)
        {
            if (verificarUsuario(email, password) != null)
            {
                // existe
                return false;
            }
            else
            {
                usuarios.Add(new Administrador(email, password));
                return true;
            }
        }


        public bool altaOrganizador(string email, string password, string nombre, string telefono, string direccion)
        {
            if (verificarUsuario(email, password) != null)
            {
                return false;
            }
            else
            {
                usuarios.Add(new Organizador(email, password, nombre, telefono, direccion));
                return true;
            }

        }

        

        public string listarUsuarios()
        {
            string devolucion = "";
            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuarios[i] is Administrador)
                {
                    devolucion += usuarios[i];
                } else
                {
                    devolucion += usuarios[i];
                }
            }
            return devolucion;
        }
        
        public void altaEvento(string email, string password)
        {
            Administrador adm = verificarUsuario(email, password);
            if (adm != null && adm is Organizador)
            {
                //casteo
                Organizador org = (Organizador)adm;
                //imprimir datos del organizador
                Console.WriteLine(org);
                Console.WriteLine("Introduce la fecha del evento");
                DateTime fecha;
                DateTime.TryParse(Console.ReadLine(), out fecha);

                if (verificarFechaEvento(fecha) == null)
                {
                    Empresa.success("La fecha se encuentra disponible");

                    Console.WriteLine("Seleccione el tipo de evento");
                    Console.WriteLine("1 - Evento estandar");
                    Console.WriteLine("2 - Evento premium");
                    int tipo = 0;
                    int.TryParse(Console.ReadLine(), out tipo);

                    if (tipo == 1 || tipo == 2)
                    {

                        Console.WriteLine("Seleccione el turno");
                        Console.WriteLine("1 - Mañana");
                        Console.WriteLine("2 - Tarde");
                        Console.WriteLine("3 - Noche");
                        int turnoNumerico = 0;
                        int.TryParse(Console.ReadLine(), out turnoNumerico);

                        Console.WriteLine("Ingrese la descripcion");
                        string descripcion = Console.ReadLine();
                        Console.WriteLine("Ingrese el nombre del cliente");
                        string cliente = Console.ReadLine();
                        Console.WriteLine("Ingrese la cantidad de asistentes");
                        int cantidadAsistentes = 0;
                        int.TryParse(Console.ReadLine(), out cantidadAsistentes);

                        if (turnoNumerico >= 1 && turnoNumerico <= 3 && descripcion != "" && cliente != "" && cantidadAsistentes > 0)
                        {
                            string turno = "";
                            switch (turnoNumerico)
                            {
                                case 1:
                                    turno = "Mañana";
                                    break;
                                case 2:
                                    turno = "Tarde";
                                    break;
                                case 3:
                                    turno = "Noche";
                                    break;
                            }

                            //listar servicios, se ingresa el nombre del servicio, se busca el mismo en la lista de servicios y se retorna
                            listarServicios();
                            Console.WriteLine("Ingrese el nombre de un servicio");
                            string nombreServicio = Console.ReadLine();
                            Servicio serv = buscarServicio(nombreServicio);
                            Console.WriteLine("Ingrese la cantidad de personas para el servicio");
                            int cantPersonasServicio = 0;
                            int.TryParse(Console.ReadLine(), out cantPersonasServicio);

                            if (serv != null && cantPersonasServicio <= cantidadAsistentes)
                            {
                                //Validacion sobre la cantidad de asistentes y la cantida de personas para el servicio

                                if (tipo == 1)
                                {

                                    //filtros especificos para eventos estandar
                                    Console.WriteLine("Ingrese la duracion (horas)");
                                    int duracion = 0;
                                    int.TryParse(Console.ReadLine(), out duracion);

                                    if (duracion > 0 && duracion <= 4 && cantidadAsistentes > 0 && cantidadAsistentes <= 10)
                                    {

                                        org.altaEvento(fecha, turno, descripcion, cliente, cantidadAsistentes, duracion, serv, cantPersonasServicio);
                                        Empresa.success("Evento estandar agregado con exito");
                                        string resultado = "x";// aqui retorno el detalle del evento;

                                    }

                                    else
                                    {

                                        Empresa.error("La duracion del evento o la cantidad de asistentes no corresponde");

                                    }

                                }

                                else
                                {
                                    //filtros para eventos premium
                                    if (cantidadAsistentes >= 0 && cantidadAsistentes <= 100)
                                    {
                                        org.altaEvento(fecha, turno, descripcion, cliente, cantidadAsistentes, serv, cantPersonasServicio);
                                        Empresa.success("Evento premium agregado con exito");

                                    }
                                    else
                                    {
                                        Empresa.error("Los eventos premium no pueden tener una cantidad de asistentes mayor a 100");
                                    }
                                }
                            }
                            else
                            {
                                Empresa.error("La cantidad de personas que asisten al evento no puede ser mayor a las personas del servicio, o el servicio ingresado no existe");
                            }
                        }
                        else
                        {
                            Empresa.error("Algun campo es esta vacio o no corresponde con lo solicitado");
                        }

                    }
                    else
                    {
                        Empresa.error("Opcion invalida");

                    }
                }
                else
                {
                    Empresa.error("Ya existe un evento para esa fecha");
                }
                
            }
            else
            {
                Empresa.error("No existe el organizador");
            }

        }

   

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


        // Metodos generales



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
        }

       /* public void listarEventos(string email, string password)
        {
            Administrador user = verificarUsuario(email, password);
            if (user != null) {
                if (user is Organizador)
                {
                    Organizador org = (Organizador)user;
                    Console.WriteLine(org.listarEventos());
                }
                else
                {
                    Empresa.error("El usuario ingresado no es un organizador");
                }
            } else
            {
                Empresa.error("No existe el usuario");
            }

        }*/

        
        private Servicio buscarServicio(string nombreServicio)
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


        public void listarServicios()
        {
            for(int i = 0; i < servicios.Count; i++)
            {
                Console.WriteLine(servicios[i]);
            }
        }
        
    }

}
