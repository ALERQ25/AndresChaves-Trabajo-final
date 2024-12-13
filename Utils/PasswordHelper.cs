using AndresChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AndresChaves.Utils
{
    public static  class PasswordHelper
    {
        public static string CrearSal()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] saltBytes = new byte[16]; // 16 bytes es un tamaño común para la sal
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes); // Convertir la sal en una cadena base64
        }
    }

    // Función para encriptar la contraseña con SHA256 y la sal
    public static string EncriptarContraseña(string contraseña, string salt)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Concatenar la sal y la contraseña
            string contrasenaConSal = salt + contraseña;

            // Convertir la cadena combinada (sal + contraseña) a bytes y generar el hash
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contrasenaConSal));

            // Convertir el hash en una cadena hexadecimal
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString(); // Retorna el hash encriptado
        }
    }

    // Función para verificar si la contraseña introducida coincide con el hash almacenado
    public static bool VerificarContraseña(string contraseñaIntroducida, string contraseñaEncriptada, byte[] salt)
    {
        // Convertir la sal de vuelta de base64 a su representación original
        string saltStr = Convert.ToBase64String(salt);

        // Encriptar la contraseña introducida con la misma sal
        string contraseñaEncriptadaIntroducida = EncriptarContraseña(contraseñaIntroducida, saltStr);

        // Comparar la contraseña encriptada introducida con la almacenada
        return contraseñaEncriptada == contraseñaEncriptadaIntroducida;
    }
}

    }
