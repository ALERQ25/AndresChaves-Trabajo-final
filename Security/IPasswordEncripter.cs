using AndresChaves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndresChaves.Security
{
    public interface IPasswordEncripter
    {
        // Encriptar con clave generada
        string Encript(string contraseña, List<byte[]> keys);

        // Desencriptar usando la clave
        //string Decript(string value, byte[] key);

        //// Encriptar y generar los hashes
        //string Encript(string contraseña, out List<byte[]> tempHashes);
        //string Decript(string encryptedPassword, byte[] key, byte[] iv);
    }
}
