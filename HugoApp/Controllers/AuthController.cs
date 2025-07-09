using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskmanager_webservice.Data;
using taskmanager_webservice.Models;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HugoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly OperacionesDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _jwtSecret;

        public AuthController(OperacionesDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _jwtSecret = _configuration["Jwt:Key"];
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.CorreoElectronico)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                message = "Login exitoso",
                token = tokenString
            });
        }
    }
}

