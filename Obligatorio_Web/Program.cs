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
                if (emp.altaAdministrador(email, password))
                {
                    success("Administrador agregado con exito");
                }
                else
                {
                    error("Ya existe un usuario con ese email");
                }
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
                if(emp.altaOrganizador(email, password, nombre, telefono, direccion))
                {
                    success("Organizador agregado con exito");
                }else
                {
                    error("Ya existe un usuario con ese email");
                }
            } else
            {
                error("Ingrese los datos nuevamente");
            }
        }

        static void listarUsuarios()
        {
            Console.WriteLine(emp.listarUsuarios());
        }

        static void registrarEvento()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();


            if (password.Length >= 8 && email.IndexOf("@eventos2017.com") > -1)
            {
               // emp.altaEvento(email, password);
            }
            else
            {
                //Empresa.error("Ingrese los datos nuevamente");
            }

        }

        static void listarServicios()
        {
            emp.listarServicios();
        }

        static void listarEventos()
        {
            Console.WriteLine("Ingrese el mail");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();
            if(email != "" && password != "")
            {
                //emp.listarEventos(email, password);
            }else
            {
                error("Datos ingresados incorrectamente");
            }
            
        }


        static void error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string result = "\n***** Error: " + message + " *****\n";
            Console.WriteLine(result);
            Console.ResetColor();
        }

        public static void success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string result = "\n***** Resultado: " + message + " *****\n";
            Console.WriteLine(result);
            Console.ResetColor();
        }

    }

}
