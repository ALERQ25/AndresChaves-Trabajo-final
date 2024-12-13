using AndresChaves.Context;
using AndresChaves.Models;
using AndresChaves.Security;
using AndresChaves.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AndresChaves.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AndresChavesContext db = new AndresChavesContext();
        private readonly IPasswordEncripter _passwordEncripter = new PasswordEncripter();

        // Metodo principal para autenticar a un usuario por el Username y Password
        public AuthResults Auth(string username, string password, out Usuarios user)
        {
            // Buscar al usuario en la base de datos por el Username
            user = db.Usuarios.Where(x => x.Usuario.Equals(username)).FirstOrDefault();

            // Si el usuario no existe se retorna un resultado de que no existe pues
            if (user == null)
                return AuthResults.NotExists;

            // Se encripta la contraseña proporcionada usando las clave almacenadas en el objeto user
            // El metodo Encript toma la Password y una lista de dos elementos: el HashKey y el HashIV del usuario
            string encriptedPassword = _passwordEncripter.Encript(password, new List<byte[]>
    {
        user.HashKey,
        user.HashIV
    });

            // Se compara la contraseña encriptada generada con la contraseña almacenada en la base de datos (PasswordHash)
            // Si no coinciden la autenticación falla y se devuelve un resultado indicando que la contraseña no coincide
            if (encriptedPassword != user.Contraseña)
                return AuthResults.PasswordNotMatch;

            // Si la contraseña encriptada coincide con la almacenada en la base de datos la autenticación es exitosa
            return AuthResults.Success;
        }

    }
}