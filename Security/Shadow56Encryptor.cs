using System;
using System.Collections.Generic;
using System.Text;

namespace AndresChaves.Security
{
    public class Shadow56Encryptor 
    {
        // Método para encriptar
        public string Encript(string value, out byte[] key)
        {
            key = GenerateKey(); // Generar una clave simple
            return EncryptWithShadow56(value, key);
        }

        // Método para desencriptar
        public string Decript(string value, byte[] key)    
        {
            return DecryptWithShadow56(value, key);
        }

        // Método para generar una "clave" simple
        private byte[] GenerateKey()
        {
            byte[] key = new byte[56]; // Tamaño de la "clave" para Shadow56
            new Random().NextBytes(key); // Llena la clave con valores aleatorios
            return key;
        }

        // Método simulado de encriptación con "Shadow56"
        private string EncryptWithShadow56(string value, byte[] key)
        {
            StringBuilder encrypted = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in value)
            {
                // Aquí realizamos una operación simple de desplazamiento
                // Esto no es un algoritmo real y es solo para propósitos demostrativos
                encrypted.Append((char)(c + key[keyIndex % key.Length]));
                keyIndex++;
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(encrypted.ToString()));
        }

        // Método simulado de desencriptación con "Shadow56"
        private string DecryptWithShadow56(string encryptedValue, byte[] key)
        {
            string base64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encryptedValue));
            StringBuilder decrypted = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in base64Decoded)
            {
                // Realizamos la operación inversa del desplazamiento para "desencriptar"
                decrypted.Append((char)(c - key[keyIndex % key.Length]));
                keyIndex++;
            }
            return decrypted.ToString();
        }

        // Este método no está implementado y lanzará una excepción si se invoca
        public string Encript(string contraseña, out List<byte[]> tempHashes)
        {
            throw new NotImplementedException();
        }

        public string Decript(string encryptedPassword, byte[] key, byte[] iv)
        {
            throw new NotImplementedException();
        }
    }
}
