using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskmanager_webservice.Data;
using taskmanager_webservice.Models;
using Microsoft.AspNetCore.Authorization;


namespace HugoApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly OperacionesDbContext _context;

        public TareasController(OperacionesDbContext context)
        {
            _context = context;
        }

        // GET: api/tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareasDelUsuario()
        {
            var correo = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(correo))
                return Unauthorized();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
            if (usuario == null)
                return NotFound("Usuario no encontrado");

            var tareas = await _context.Tareas
                .Where(t => t.UsuarioId == usuario.Id)
                .ToListAsync();

            return Ok(tareas);
        }

        // POST: api/tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            try
            {
                var correo = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(correo))
                    return Unauthorized("Token no válido o no proporcionado.");

                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
                if (usuario == null)
                    return NotFound("Usuario no encontrado.");

                tarea.UsuarioId = usuario.Id;

                _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTareasDelUsuario), new { id = tarea.Id }, tarea);
            }
            catch (Exception ex)
            {
                // Esto te ayudará a ver el error específico en los logs
                return StatusCode(500, new { message = "Error al crear la tarea", error = ex.Message });
            }
        }


        // PUT: api/tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarea).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
