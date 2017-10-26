using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_Dominio
{
    public class Administrador
    {
        protected string email;
        protected string password;
        protected string rol;

        public string Email { get { return this.email; } }
        public string Password { get { return this.password; } }

        public Administrador(string email, string password)
        {
            this.email = email;
            this.password = password;
            this.rol = "Administrador";
        }

        public override string ToString()
        {
            string resultado = "\n:::::::::::::::::::::::::::\n Rol: " + this.rol + "\n Email: " + this.email + "\n Password: " + this.password + "\n:::::::::::::::::::::::::::\n";
            return resultado;
        }

        
    }
}
