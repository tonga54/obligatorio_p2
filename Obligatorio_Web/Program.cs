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
                emp.altaAdministrador(email, password);
            }else
            {
                emp.error("Ingrese los datos nuevamente");
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
                emp.altaOrganizador(email, password,nombre,telefono,direccion);
            }else
            {
                emp.error("Ingrese los datos nuevamente");
            }
        }

        static void listarUsuarios()
        {
            emp.listarUsuarios();
        }

        static void registrarEvento()
        {
            Console.WriteLine("Ingrese el email");
            string email = Console.ReadLine();
            Console.WriteLine("Ingrese el password");
            string password = Console.ReadLine();

            if (password.Length >= 8 && email.IndexOf("@eventos2017.com") > -1)
            {
                emp.altaEvento(email, password);
            }
            else
            {
                emp.error("Ingrese los datos nuevamente");
            }

        }

    }
}
