using AndresChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresChaves.Services
{
    // Cambio de 'internal' a 'public' para permitir el acceso a la interfaz desde fuera de la ensambladura
    public interface IAuthorizationService
    {
        // Método para autenticar al usuario
        AuthResults Auth(string usuario, string contraseña, out Usuarios usuarioObj);
        //AuthResults Auth(string usuario, string contraseña, out Usuarios usuarioObj, List<byte[]> hashes);
    }

    // Enumeración para los posibles resultados de la autenticación
    public enum AuthResults
    {
        Success,          // Autenticación exitosa
        PasswordNotMatch, // Contraseña incorrecta
        NotExists,        // El usuario no existe
        Error             // Ocurrió un error inesperado
    }
}
