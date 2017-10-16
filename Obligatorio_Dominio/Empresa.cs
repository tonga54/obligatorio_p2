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

        public void altaAdministrador(string email,string password)
        {
            if (verificarUsuario(email, password) != null)
            {
                error("El usuario ya existe");
            }else
            {
                usuarios.Add(new Administrador(email, password));
                success("Administrador agregado con exito");
            }
            
        }
        
        public void altaOrganizador(string email, string password, string nombre, string telefono, string direccion)
        {
            if (verificarUsuario(email, password) != null)
            {
                error("El usuario ya existe");
            }
            else
            {
                usuarios.Add(new Organizador(email, password, nombre, telefono, direccion));
                success("Organizador agregado con exito");
            }
            
        }

        public Administrador verificarUsuario(string email, string password) {
            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuarios[i].Email == email && usuarios[i].Password == password)
                {
                    if (usuarios[i] is Organizador) { 
                        Organizador org = (Organizador)usuarios[i];
                        return org;
                    }
                }
            }

            return null;
        }

        public void listarUsuarios()
        {
            for(int i = 0; i < usuarios.Count; i++)
            {
                if(usuarios[i] is Administrador)
                {
                    Console.WriteLine(usuarios[i]);
                }else
                {
                    Console.WriteLine(usuarios[i]);
                }
            }
        }

        public void altaEvento(string email,string password)
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
                if (!verificarFechaEvento(fecha))
                {
                    success("La fecha se encuentra disponible");

                    Console.WriteLine("Seleccione el tipo de evento");
                    Console.WriteLine("1 - Evento estandar");
                    Console.WriteLine("2 - Evento premium");
                    int tipo = 0;
                    int.TryParse(Console.ReadLine(), out tipo);

                    if(tipo == 1 || tipo == 2)
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

                        if (turnoNumerico >= 1 && turnoNumerico <= 3 && descripcion != "" && cliente != "" && cantidadAsistentes > 0 )
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

                            if (tipo == 1)
                            {

                                //filtros especificos para eventos estandar
                                Console.WriteLine("Ingrese la duracion (horas)");
                                int duracion = 0;
                                int.TryParse(Console.ReadLine(), out duracion);

                                if (duracion > 0 && duracion <= 4 && cantidadAsistentes <= 10)
                                {
                                    org.altaEvento(fecha, turno, descripcion, cliente, cantidadAsistentes,duracion);
                                    success("Evento estandar agregado con exito");
                                }
                                else
                                {
                                    error("La duracion del evento o la cantidad de asistentes no corresponde");
                                }

                            }
                            else
                            {
                                //filtros para eventos premium
                                if(cantidadAsistentes <= 100)
                                {
                                    org.altaEvento(fecha, turno, descripcion, cliente, cantidadAsistentes);
                                    success("Evento premium agregado con exito");
                                }else
                                {
                                    error("Los eventos premium no pueden tener una cantidad de asistentes mayor a 100");
                                }
                            }

                        }
                        else
                        {
                            error("Algun campo es esta vacio o no corresponde con lo solicitado");
                        }

                    }
                    else
                    {
                        error("Opcion invalida");

                    }
                }
                else
                {
                    error("Ya existe un evento para esa fecha");
                }

            }
            else
            {
                error("No existe el organizador");
            }
        }


        public bool verificarFechaEvento(DateTime fecha)
        {
            for(int i = 0; i < usuarios.Count; i++)
            {
                Administrador adm = usuarios[i];
                if(adm is Organizador)
                {
                    Organizador org = (Organizador)adm;
                    return org.verificarFecha(fecha);
                }
            }
            return false;
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

        }




        public void error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string result = "\n***** Error: " + message + " *****\n";
            Console.WriteLine(result);
            Console.ResetColor();
        }

        public void success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string result = "\n***** Resultado: " + message + " *****\n";
            Console.WriteLine(result);
            Console.ResetColor();
        }


    }
}
