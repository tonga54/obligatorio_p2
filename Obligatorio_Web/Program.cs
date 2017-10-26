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

        static void registrarAdministrador()
        {
            Console.Clear();
            Console.WriteLine("\n---------- REGISTRAR ADMINISTRADOR ----------\n");
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
            Console.Clear();
            Console.WriteLine("\n---------- REGISTRAR ORGANIZADOR ----------\n");
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
            Console.Clear();
            Console.WriteLine("\n---------- REGISTRAR EVENTO ----------\n");
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

                            /* 
                             Imprimo la lista de servicios, creo dos listas una de tipo Servicio y la otra de tipo entero
                             entro en un while donde su condicion de salida es que la variable de tipo string "nombreServicio" sea "salir"
                             mientras eso no se cumpla, el usuario estara ingresando nombres de servicios los cuales seran verificados
                             en la lista de servicios que posee empresa. Para la lista de enteros cantPersonasServicio se valida que 
                             el usuario ingrese correctamente algo que sea numerico y que sea menor a la cantidad de personas
                             asistentes al evento. Una vez cumplido todo esto quedan ambas listas una con servicios y la otra con la cantidad de asistentes
                             para el mismo.  
                             */

                            Console.WriteLine(emp.listarServicios());
                            string nombreServicio = "";
                            int cantPersonasServicio = 0;
                            List<Servicio> servicios = new List<Servicio>();
                            List<int> cantPersonasServicioLista = new List<int>();

                            while (nombreServicio != "salir")
                            {
                                success("Ingrese 'salir' para dejar de agregar servicios");

                                Console.WriteLine("Ingrese el nombre de un servicio");
                                nombreServicio = Console.ReadLine();

                                if (nombreServicio.ToLower() != "salir" && nombreServicio != "")
                                {
                                    Console.WriteLine("Ingrese la cantidad de personas para el servicio MAX(" + cantidadAsistentes + ")");
                                    string cantPersonas = Console.ReadLine();

                                    if (verificarNumerico(cantPersonas))
                                    {
                                        int.TryParse(cantPersonas, out cantPersonasServicio);
                                        if (cantPersonasServicio <= cantidadAsistentes && cantPersonasServicio > 0)
                                        {
                                            Servicio servicio = emp.buscarServicio(nombreServicio);
                                            if (servicio != null)
                                            {
                                                servicios.Add(servicio);
                                                cantPersonasServicioLista.Add(cantPersonasServicio);
                                            }
                                            else
                                            {
                                                error("No existe el servicio");
                                            }
                                        }
                                        else
                                        {
                                            error("La cantidad de asistentes debe ser mayor a 0 y menor a la cantidad de asistentes al evento");
                                        }
                                    }
                                    else
                                    {
                                        error("La cantidad de personas para el servicio debe ser numerico");
                                    }
                                }
                            }

                            if(servicios.Count > 0 || cantPersonasServicioLista.Count > 0)
                            {

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
                            error("No se pueden registrar eventos sin servicios");
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
            Console.Clear();
            Console.WriteLine("\n---------- LISTAR USUARIOS ----------\n");
            Console.WriteLine(emp.listarUsuarios());
        }

        static void listarServicios()
        {
            Console.Clear();
            Console.WriteLine("\n---------- LISTAR SERVICIOS ----------\n");
            string devolucion = emp.listarServicios();
            Console.WriteLine(devolucion);
        }

        static void listarEventos()
        {
            Console.Clear();
            Console.WriteLine("\n---------- LISTAR EVENTOS ----------\n");
            Console.WriteLine("Ingrese el mail");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese la contraseña");
            string password = Console.ReadLine();
            Administrador user = emp.verificarUsuario(email,password);
            if (user != null && user is Organizador)
            {
                Organizador org = (Organizador)user;
                Console.WriteLine("\n" + emp.listarEventos(org,email) + "\n");
            }else
            {
                error("No existe el organizador");
            }

        }

        static void agregarServicioAEvento()
        {
            Console.Clear();
            Console.WriteLine("\n---------- AÑADIR SERVICIOS A EVENTOS ----------\n");
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese la contraseña");
            string password = Console.ReadLine();
            Administrador adm = emp.verificarUsuario(email, password);
            if (adm is Organizador)
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
                        List<Servicio> servicios = new List<Servicio>();
                        List<int> cantPersonasServicioLista = new List<int>();
                        //Tengo algunas dudas con esta variable ya que no se si es muy correcto hacer
                        //esto, es la unica forma que encontre de poder consultar la cantidad de personas
                        //para un evento, ya que podria haberlo hecho simplemente llamando a la property
                        //del evento, pero como Program no tiene acceso a dicho contenido lo que hice
                        //fue como ir paso a paso de clase a clase hasta poder retornar dicho valor.
                        int cantPersonasEvento = emp.cantAsistentesEvento(org, ev);
                        while (nombreServicio != "salir")
                        {
                            success("Ingrese 'salir' para dejar de agregar servicios");
                            Console.WriteLine("Ingrese el nombre de un servicio");
                            nombreServicio = Console.ReadLine();

                            if (nombreServicio.ToLower() != "salir" && nombreServicio != "")
                            {
                                Console.WriteLine("Ingrese la cantidad de personas para el servicio MAX(" + cantPersonasEvento + ")");
                                string cantPersonas = Console.ReadLine();
                                if (verificarNumerico(cantPersonas))
                                {
                                    int.TryParse(cantPersonas, out cantPersonasServicio);
                                    //if (cantPersonasServicio > 0 && cantPersonasServicio <= ev.CantAsistentes)
                                    
                                    if (cantPersonasServicio > 0 && cantPersonasServicio <= cantPersonasEvento)
                                    {
                                        Servicio serv = emp.buscarServicio(nombreServicio);
                                        if (serv != null)
                                        {
                                            servicios.Add(serv);
                                            cantPersonasServicioLista.Add(cantPersonasServicio);
                                        }
                                        else
                                        {
                                            error("No existe el servicio");
                                        }
                                    }
                                    else
                                    {
                                        error("La cantidad de asistentes debe ser mayor a 0 y menor a la cantidad de asistentes al evento");
                                    }
                                }
                                else
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
            }
            else
            {
                error("No existe el organizador");
            }
        }

        static void modificarPrecioLimpieza()
        {
            Console.Clear();
            Console.WriteLine("\n---------- MODIFICAR EL PRECIO DE LIMPIEZA ----------\n");
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
            Console.Clear();
            Console.WriteLine("\n---------- MODIFICAR EL PRECIO DE AUMENTO ----------\n");
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

        // --------------------------   VALIDACIONES   --------------------------

        //Este metodo recibe un string y lo recorre caracter por caracter,
        //si alguno de ellos no es un digito retorna false.
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

        //Este metodo recibe un string y un caracter, el mismo a traves del bucle
        //for, recorre el string y cuenta la cantidad de veces que aparece el caracter
        //en el mismo.
        static int contarCaracter(string cadena, char caracter)
        {
            int contador = 0;
            for (int i = 0; i < cadena.Length; i++)
            {
                if (cadena[i] == caracter)
                {
                    contador++;
                }
            }
            return contador;
        }

        static bool verificarEmail(string email)
        {
            bool bandera = false;
            //Si hay un @ y un .
            if (contarCaracter(email, '@') == 1 && contarCaracter(email, '.') == 1)
            {
                //Busco la posicion de dicho 'punto' y 'arroba'
                int arroba = email.IndexOf('@');
                int punto = email.IndexOf('.');

                //Si el arroba no esta ni al principio ni al final y el punto esta mas adelante que el arroba.
                if ((arroba != 0 && arroba != email.Length - 1) && punto > arroba)
                {
                    //Luego del '.' tienen que haber por lo menos 2 caracteres.
                    if ((email.Length - (punto + 1)) >= 2) 
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
            for (int i = 0; i < password.Length; i++)
            {
                //Recorre toda la cadena y utilizo un contador ya que solo se permite un signo
                if (char.IsPunctuation(password[i]))
                {
                    simbolo++;
                }
                //Hago lo mismo para la mayuscula, la diferencia es que utilizo un booleano
                //solo necesito validar si hay por lo menos una mayuscula, entonces utilizo una bandera.
                else if (char.IsUpper(password[i]))
                {
                    mayuscula = true;
                }
            }

            //Si hubo mayusculas y un solo signo.
            if (mayuscula && simbolo == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool verificarNombre(string nombre)
        {
            bool bandera = true;
            int i = 0;
            if (nombre.Length == 3)
            {
                // En el caso de que haya 3 caracteres, seria 'A(espacio)A', entonces como el caso minimo
                //serian 3 caracteres, lo harcodeo.
                if (!(char.IsLetter(nombre[0]) && char.IsWhiteSpace(nombre[1]) && char.IsLetter(nombre[2]))) 
                {
                    bandera = false;
                }
            }
            else
            {
                //Si no, es decir si hay mas de 3 caracteres, entonces recorro el string
                while (i < nombre.Length && bandera == true)
                {
                    //Si el caracter no es una letra o un espacio en esta todo correcto.  
                    if (!(char.IsLetter(nombre[i]) || char.IsWhiteSpace(nombre[i])))
                    {
                         bandera = false;
                    }
                i++;
                }
            }

            return bandera;
        }

        // --------------------------   METODOS GENERICOS   --------------------------

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