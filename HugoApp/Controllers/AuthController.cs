using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskmanager_webservice.Data;
using taskmanager_webservice.Models;
using System.Linq;

namespace HugoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly OperacionesDbContext _context;

        public AuthController(OperacionesDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario registro)
        {
            // Verificar si el correo ya existe
            if (_context.Usuarios.Any(u => u.CorreoElectronico == registro.CorreoElectronico))
            {
                return BadRequest("Correo ya registrado");
            }

            // Hashear la contraseña antes de guardar
            registro.SetPassword(registro.Contraseña);

            // Guardar usuario en la base de datos
            _context.Usuarios.Add(registro);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado correctamente" });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginData)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.CorreoElectronico == loginData.CorreoElectronico)
                .FirstOrDefaultAsync();

            if (usuario == null || !usuario.VerifyPassword(loginData.Contraseña))
            {
                return Unauthorized("Correo o contraseña incorrectos");
            }

            // Aquí podrías devolver un JWT Token (pero por ahora, devolveremos un mensaje)
            return Ok(new { Message = "Login exitoso" });
        }
    }
}

