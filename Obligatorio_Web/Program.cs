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
            while (opcion != 10)
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
            Console.WriteLine("7 - Añadir servicios a eventos");
            Console.WriteLine("8 - Modificar el precio de la Limpieza");
            Console.WriteLine("9 - Modificar el precio de aumento");
            Console.WriteLine("10 - Salir");
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
                case 7:
                    agregarServicioAEvento();
                    break;
                case 8:
                    modificarPrecioLimpieza();
                    break;
                case 9:
                    modificarPrecioAumento();
                    break;
                default:
                    break;
            }
        }

        static void agregarServicioAEvento()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese la contraseña");
            string password = Console.ReadLine();
            Administrador adm = emp.verificarUsuario(email, password);
            if(adm is Organizador)
            {
                Organizador org = (Organizador)adm;
                Console.WriteLine("Ingrese la fecha del evento");
                DateTime fecha;
                DateTime.TryParse(Console.ReadLine(), out fecha);
                if (fecha.Date >= DateTime.Now.Date)
                {
                    Evento ev = emp.verificarFechaEvento(fecha, org);
                    if (ev != null)
                    {
                        Console.WriteLine(ev.ToString());
                        listarServicios();
                        string nombreServicio = "";
                        int cantPersonasServicio = 0;
                        List<string> servicios = new List<string>();
                        List<int> cantPersonasServicioLista = new List<int>();

                        while (nombreServicio != "salir")
                        {
                            success("Ingrese 'salir' para dejar de agregar servicios");
                            Console.WriteLine("Ingrese el nombre de un servicio");
                            nombreServicio = Console.ReadLine();

                            if (nombreServicio.ToLower() != "salir" && nombreServicio != "")
                            {
                                Console.WriteLine("Ingrese la cantidad de personas para el servicio");
                                string cantPersonas = Console.ReadLine();
                                if (verificarNumerico(cantPersonas))
                                {
                                    int.TryParse(cantPersonas, out cantPersonasServicio);
                                    if(cantPersonasServicio > 0 && cantPersonasServicio <= ev.CantAsistentes)
                                    {
                                        servicios.Add(nombreServicio);
                                        cantPersonasServicioLista.Add(cantPersonasServicio);
                                    }else
                                    {
                                        error("La cantidad de asistentes debe ser mayor a 0 y menor a la cantidad de asistentes al evento");
                                    }
                                }else
                                {
                                    error("La cantidad de personas para el servicio debe ser numerico");
                                }
                                
                            }

                         }
                        Console.WriteLine(emp.agregarServicioAEvento(org, ev, servicios, cantPersonasServicioLista));
                    }
                    else
                    {
                        error("No existe un evento con esa fecha");
                    }
                }
                else
                {
                    error("La fecha ya ah transcurrido");
                }
            }else
            {
                error("No existe el organizador");
            }
        }

        static bool verificarNumerico(string cadena)
        {
            int i = 0;
            bool bandera = true;
            while (i < cadena.Length && bandera)
            {
                if (!char.IsDigit(cadena[i]))
                {
                    bandera = false;
                }
                i++;
            }
            return bandera;
        }

        static int contarCaracter(string cadena, char caracter)
        {
            int contador = 0;
            for(int i = 0; i < cadena.Length; i++)
            {
                if(cadena[i] == caracter)
                {
                    contador++;
                }
            }
            return contador;
        }

        static bool verificarEmail(string email)
        {
            /// gaston@eventos2017.com"
            bool bandera = false;
            if (contarCaracter(email, '@') == 1 && contarCaracter(email,'.') == 1) // si hay un @ y un .
            {
                int arroba = email.IndexOf('@');
                int punto = email.IndexOf('.');

                if ((arroba != 0 && arroba != email.Length - 1) && punto > arroba) //si el arroba no esta ni al principio ni al final y el punto esta mas adelante que el arroba
                {
                    if ((email.Length - (punto + 1)) >= 2) // luego del '.' quedan al menos 2 caracteres 
                    {
                        bandera = true;
                    }
                }
            }
            return bandera;
        }

        static bool verificarPassword(string password)
        {
            bool mayuscula = false;
            int simbolo = 0;
            for(int i = 0; i < password.Length; i++)
            {
                if (char.IsPunctuation(password[i]))//como solo se permite un signo, para detectar varios uso un contador
                {
                    simbolo++;
                }
                else if (char.IsUpper(password[i]))//si hay una mayuscula
                {
                    mayuscula = true;
                }
            }

            if(mayuscula && simbolo == 1)//si hubo mayusculas y un solo simbolo todo OK
            {
                return true;
            }else
            {
                return false;
            }
        }

        static bool verificarNombre(string nombre)
        {
            bool bandera = true;
            int i = 0;
            if(nombre.Length == 3)
            {
                if(!(char.IsLetter(nombre[0]) && char.IsWhiteSpace(nombre[1]) && char.IsLetter(nombre[2]))) // en el caso de que haya 3 caracteres, seria 'A(espacio)A'
                {
                    bandera = false;
                }
            }else
            {
                while (i < nombre.Length && bandera == true)
                {
                    if (char.IsLetter(nombre[i]) || char.IsWhiteSpace(nombre[i])) //si el caracter es una letra o un espacio en blanco todo ok
                    {
                        bandera = true;
                    }
                    else // si es otra cosa entonces false termino el bucle y salgo.
                    {
                        bandera = false;
                    }
                    i++;
                }
            }
            
            return bandera;
        }

        static void registrarAdministrador()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();
            

            if (verificarPassword(password) && password.Length >= 8 && verificarEmail(email))
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

            if (verificarPassword(password) && password.Length >= 8 && verificarEmail(email) && verificarNombre(nombre) && nombre.Length >= 3 && telefono != "" && direccion != "")
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

            if(emp.verificarUsuario(email,password) != null)
            {
                success("Se a ingresado con exito");
                Console.WriteLine("Introduce la fecha del evento");
                DateTime fecha;
                DateTime.TryParse(Console.ReadLine(), out fecha);
                fecha = fecha.Date;
                DateTime fechaMax = new DateTime(DateTime.Now.Year+1, DateTime.Now.Month, DateTime.Now.Day);
                if (fecha.Date >= DateTime.Now.Date && fecha.Date <= fechaMax.Date)
                {
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
                             el valor 1 seteado, ya que en el caso de que no se ingrese cant personas para el servicio deseado. 
                     
                             Creo dos listas una de strings la cual va a poseer el nombre de cada uno de los servicios que vaya introduciendo el usuario
                             como a su vez la de enteros que contendra la cantidad de personas para dicho servicio. Todo esto para luego
                             hacer un mapeo y 'sincronizar' el servicio con la cantidad de personas
                     
                             Dentro del while la condicion de salida es que el usuario ingrese "salir" mientras ello no se cumpla se pedira el nombre
                             del servicio y la cantidad de personas. 
                             
                             El sentido de hacer esta implementacion un poco rebuscada es para intentar que program trabaje lo menor posible
                             con objetos que devuelva empresa, ya que por seguridad en mi entender es lo mejor.
                             */


                            Console.WriteLine(emp.listarServicios());
                            string nombreServicio = "";
                            int cantPersonasServicio = 0;
                            List<string> servicios = new List<string>();
                            List<int> cantPersonasServicioLista = new List<int>();

                            while (nombreServicio != "salir")
                            {
                                success("Ingrese 'salir' para dejar de agregar servicios");
                                Console.WriteLine("Ingrese el nombre de un servicio");
                                nombreServicio = Console.ReadLine();

                                if (nombreServicio.ToLower() != "salir" && nombreServicio != "")
                                {
                                    Console.WriteLine("Ingrese la cantidad de personas para el servicio");
                                    string cantPersonas = Console.ReadLine();
                                    if (verificarNumerico(cantPersonas))
                                    {
                                        int.TryParse(cantPersonas, out cantPersonasServicio);
                                        if (cantPersonasServicio <= cantidadAsistentes)
                                        {
                                            servicios.Add(nombreServicio);
                                            cantPersonasServicioLista.Add(cantPersonasServicio);
                                        }
                                        else
                                        {
                                            error("La cantidad de personas para el servicio debe ser menor o igual a la de asistentes al evento");
                                        }
                                    }
                                    else
                                    {
                                        error("La cantidad de personas para el servicio debe ser numerico");
                                    }
                                }
                            }
                            if (tipo == 1)
                            {
                                //Filtros para eventos estandar
                                Console.WriteLine("Ingrese la duracion (horas)");
                                int duracion = 0;
                                int.TryParse(Console.ReadLine(), out duracion);

                                if (duracion > 0 && duracion <= 4 && cantidadAsistentes > 0 && cantidadAsistentes <= 10)
                                {
                                    Console.WriteLine(emp.altaEvento(email, password, fecha, turno, descripcion, cliente, cantidadAsistentes, duracion, servicios, cantPersonasServicioLista));
                                }
                                else
                                {
                                    error("La duracion del evento o la cantidad de asistentes no corresponde");
                                }
                            }
                            else
                            {
                                //Filtros para eventos premium
                                if (cantidadAsistentes > 0 && cantidadAsistentes <= 100)
                                {
                                    Console.WriteLine(emp.altaEvento(email, password, fecha, turno, descripcion, cliente, cantidadAsistentes, servicios, cantPersonasServicioLista));
                                    
                                }
                                else
                                {
                                    error("Cantidad de asistentes incorrecto, debe ser mayor a 0 y/o menor o igual a 100");
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
                    error("La fecha del evento no puede ser menor a la fecha acual / Solo se permiten agregar eventos con la fecha no superior a un año de diferencia con la actual");
                }
            }
            else
            {
                error("El usuario no existe");
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

        static void modificarPrecioLimpieza()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();

            Console.WriteLine("Ingrese el precio de la limpieza para eventos estandar");
            string precio = Console.ReadLine();
            if (verificarNumerico(precio))
            {
                decimal precioLimpieza = 0;
                decimal.TryParse(precio, out precioLimpieza);
                Console.WriteLine(emp.modificarPrecioLimpieza(email,password,precioLimpieza));
            }else
            {
                error("El precio debe ser numerico");
            }
        }

        static void modificarPrecioAumento()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();

            Console.WriteLine("Ingrese el precio de aumento para eventos premium");
            string precio = Console.ReadLine();
            if (verificarNumerico(precio))
            {
                decimal precioLimpieza = 0;
                decimal.TryParse(precio, out precioLimpieza);
                Console.WriteLine(emp.modificarPrecioAumento(email, password, precioLimpieza));
            }
            else
            {
                error("El precio debe ser numerico");
            }
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