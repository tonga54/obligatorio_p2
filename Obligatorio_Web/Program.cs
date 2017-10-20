using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio_Dominio;

namespace Obligatorio_Web
{
    class Program
    {
        static Empresa emp = new Empresa();

        static void Main(string[] args)
        {
            int opcion = 0;
            while (opcion != 7)
            {
                mostrarMenu();
                Console.WriteLine("Ingrese una opcion:");
                int.TryParse(Console.ReadLine(), out opcion);
                accionesMenu(opcion);
            }
        }

        static void mostrarMenu()
        {
            Console.WriteLine("1 - Registrar administrador");
            Console.WriteLine("2 - Registrar organizador");
            Console.WriteLine("3 - Listar usuarios");
            Console.WriteLine("4 - Registrar evento");
            Console.WriteLine("5 - Listar eventos");
            Console.WriteLine("6 - Listar servicios");
        }

        static void accionesMenu(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    registrarAdministrador();
                    break;
                case 2:
                    registrarOrganizador();
                    break;
                case 3:
                    listarUsuarios();
                    break;
                case 4:
                    registrarEvento();
                    break;
                case 5:
                    listarEventos();
                    break;
                case 6:
                    listarServicios();
                    break;
                default:
                    break;
            }
        }

        static void registrarAdministrador()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();

            if (password.Length >= 8 && email.IndexOf("@eventos2017.com") > -1)
            {
                string devolucion = emp.altaAdministrador(email, password);
                Console.WriteLine("\n" + devolucion + "\n");
            }
            else
            {
                error("Ingrese los datos nuevamente");
            }
        }

        static void registrarOrganizador()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el telefono");
            string telefono = Console.ReadLine();
            Console.WriteLine("Ingrese la direccion");
            string direccion = Console.ReadLine();

            if (password.Length >= 8 && email.IndexOf("@eventos2017.com") > -1 && nombre != "" && telefono != "" && direccion != "")
            {
                string devolucion = emp.altaOrganizador(email, password, nombre, telefono, direccion);
                Console.WriteLine("\n" + devolucion + "\n");
            } else
            {
                error("Ingrese los datos nuevamente");
            }

        }

        static void registrarEvento()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();
            Console.WriteLine("Introduce la fecha del evento");
            DateTime fecha;
            DateTime.TryParse(Console.ReadLine(), out fecha);
            fecha = fecha.Date;

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

                        /* Imprimo la lista de servicios, creo una variable de tipo string nombreServicio y cantPersonasServicio entero con
                         el valor 1 seteado, ya que en el caso de que no se ingrese cant personas para el servicio deseado, el valor por default
                         sera uno. 
                     
                         Creo dos listas una de strings la cual va a poseer el nombre de cada uno de los servicios que vaya introduciendo el usuario
                         como a su vez la de enteros que contendra la cantidad de personas para dicho servicio. Todo esto para luego
                         hacer un mapeo y 'sincronizar' el servicio con la cantidad de personas
                     
                         Dentro del while la condicion de salida es que el usuario ingrese "salir" mientras ello no se cumple se pedira el nombre
                         del servicio y la cantidad de personas. */


                        Console.WriteLine(emp.listarServicios());
                        string nombreServicio = "";
                        int cantPersonasServicio = 1;
                        List<string> servicios = new List<string>();
                        List<int> cantPersonasServicioLista = new List<int>();

                        while (nombreServicio != "salir")
                        {
                            Console.WriteLine("\nIngrese 'salir' para dejar de agregar servicios\n");
                            Console.WriteLine("Ingrese el nombre de un servicio");
                            nombreServicio = Console.ReadLine();
                            //nombreServicio = 
                            if(nombreServicio != "salir")
                            {
                                servicios.Add(nombreServicio);
                                Console.WriteLine("Ingrese la cantidad de personas para el servicio");
                                int.TryParse(Console.ReadLine(), out cantPersonasServicio);
                                cantPersonasServicioLista.Add(cantPersonasServicio);
                            /*
                                int i = 0;
                                bool bandera = false;
                                string cantPers = Console.ReadLine();
                                while (i < cantPers.Length && bandera == false)
                                {
                                    if (Char.IsLetter(cantPers, i))
                                    {
                                        bandera = true;
                                    }
                                i++;
                                }

                            if (bandera)
                            {
                                error("Solo se permiten numericos");
                            }else
                            {
                                int.TryParse(cantPers, out cantPersonasServicio);
                            }*/

                            }
                        }
                    
                        if (!verificarNumeroDeAsistentesMenorANumeroServicio(cantPersonasServicioLista, cantidadAsistentes))
                        {
                            if (tipo == 1)
                            {

                                //Filtros para eventos estandar
                                Console.WriteLine("Ingrese la duracion (horas)");
                                int duracion = 0;
                                int.TryParse(Console.ReadLine(), out duracion);

                                if (duracion > 0 && duracion <= 4 && cantidadAsistentes > 0 && cantidadAsistentes <= 10)
                                {
                                    string resultado = emp.altaEvento(email,password,fecha, turno, descripcion, cliente, cantidadAsistentes, duracion, servicios, cantPersonasServicioLista);
                                    Console.WriteLine(resultado);
                                }
                                else
                                {
                                    error("La duracion del evento o la cantidad de asistentes no corresponde");
                                }
                            }
                            else
                            {
                                //Filtros para eventos premium
                                if (cantidadAsistentes >= 0 && cantidadAsistentes <= 100)
                                {
                                    string resultado = emp.altaEvento(email,password,fecha, turno, descripcion, cliente, cantidadAsistentes, servicios, cantPersonasServicioLista);
                                    Console.WriteLine(resultado);
                                }
                                else
                                {
                                    error("Los eventos premium no pueden tener una cantidad de asistentes mayor a 100");
                                }
                            }
                        }
                        else
                        {
                            error("La cantidad de personas que asisten al evento no puede ser mayor a las personas del servicio / Ingresó una cifra menor a 0");
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



        static bool verificarUsuario(string email, string password)
        {
            if(password.Length >= 8 && email.IndexOf("@eventos2017.com") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
         }

        static void listarUsuarios()
        {
            Console.WriteLine(emp.listarUsuarios());
        }

        static void listarServicios()
        {
            string devolucion = emp.listarServicios();
            Console.WriteLine(devolucion);
        }

        static void listarEventos()
        {
            Console.WriteLine("Ingrese el mail");
            string email = Console.ReadLine();
            if(email != "")
            {
                string devolucion = emp.listarEventos(email);
                Console.WriteLine("\n" + devolucion + "\n");
            }else
            {
                error("Datos ingresados incorrectamente");
            }

        }


        //Este metodo se encarga de verificar uno por uno, que la cantidad de personas para cada servicio sea menor que la cantidad
        //de asistentes al evento.
        static bool verificarNumeroDeAsistentesMenorANumeroServicio (List<int>cantPersonasServicio,int cantAsistentes)
        {
            bool bandera = false;
            int i = 0;
            while (i < cantPersonasServicio.Count && bandera == false)
            {
                if (cantPersonasServicio[i] >= cantAsistentes || cantPersonasServicio[i] < 1)
                {
                    bandera = true;
                }
                i++;
            }
            return bandera;
        }
   
        static void error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string result = "\n***** Error: " + message + " *****\n";
            Console.WriteLine(result);
            Console.ResetColor();
        }

        static void success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string result = "\n***** Resultado: " + message + " *****\n";
            Console.WriteLine(result);
            Console.ResetColor();
        }
    }
}