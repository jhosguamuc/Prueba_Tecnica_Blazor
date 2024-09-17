using front.Models;
using System.Net.Http;

namespace front.Services.Interface
{
    public interface ITareaService
    {
        Task<Tarea> GetTareaById(int id);
        Task<List<Tarea>> GetTareas(int usuarioId);
        Task<List<Prioridad>> GetPrioridad();
        Task CrearTarea(Tarea tarea);
        Task ActualizarTarea(Tarea tarea);
        Task EliminarTarea(int id);
        Task MarcarTareaCompletada(Tarea tarea);
    }
}
