using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace taskmanager_webservice.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contraseña { get; set; }

        // Método para generar el hash de la contraseña
        public void SetPassword(string password)
        {
            // Usando el algoritmo PBKDF2 para hash de la contraseña
            Contraseña = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(CorreoElectronico), // Sal para evitar ataques de rainbow tables
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        // Método para verificar la contraseña
        public bool VerifyPassword(string password)
        {
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(CorreoElectronico),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return Contraseña == hashedPassword;
        }
    }
}

