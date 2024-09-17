using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetTareaUsuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<TblTarea>>> GetTareas(int usuarioId)
        {
            return await _context.TblTareas.Where(c => c.IdUsuario == usuarioId).Include(t => t.Prioridad).Include(t => t.Usuario).ToListAsync();
        }

        [HttpGet("GetPrioridad")]
        public async Task<ActionResult<IEnumerable<TblPrioridad>>> GetPrioridad()
        {
            return await _context.TblPrioridad.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblTarea>> GetTarea(int id)
        {
            var tarea = await _context.TblTareas.FindAsync(id);
            if (tarea == null) return NotFound();

            return tarea;
        }

        [HttpPost]
        public async Task<ActionResult<TblTarea>> PostTarea(TblTarea tarea)
        {
            try
            {
                tarea.Created_At = DateTime.Now;
                tarea.Updated_At = DateTime.Now;
                tarea.Prioridad = _context.TblPrioridad.FirstOrDefault(c => c.Id == tarea.IdPrioridad);
                tarea.Usuario = _context.TblUsuarios.FirstOrDefault(c => c.Id == tarea.IdUsuario);
                _context.TblTareas.Add(tarea);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, TblTarea tarea)
        {
            if (id != tarea.Id) return BadRequest();

            var update = await _context.TblTareas.FindAsync(id);

            update.Updated_At = DateTime.Now;
            update.Finalizado = tarea.Finalizado;
            update.Titulo = tarea.Titulo;
            update.Descripcion = tarea.Descripcion;
            update.IdPrioridad = tarea.IdPrioridad;
            update.Tags = tarea.Tags;
            update.FechaVencimiento = tarea.FechaVencimiento;


            _context.Entry(update).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.TblTareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.TblTareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
