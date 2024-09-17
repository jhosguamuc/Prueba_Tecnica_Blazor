using front.Models;
using front.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace front.Services
{
    public class TareaService : ITareaService
    {
        private readonly string urlBase = "https://localhost:7014/api/";
        private readonly HttpClient _httpClient;
        public TareaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ActualizarTarea(Tarea tarea)
        {
            await _httpClient.PutAsJsonAsync($"{urlBase}Tareas/{tarea.Id}", tarea);
        }

        public async Task CrearTarea(Tarea tarea)
        {
            await _httpClient.PostAsJsonAsync($"{urlBase}Tareas", tarea);
        }

        public async Task EliminarTarea(int id)
        {
            await _httpClient.DeleteAsync($"{urlBase}Tareas/{id}");
        }

        public async Task<List<Tarea>> GetTareas(int usuarioId)
        {
            return await _httpClient.GetFromJsonAsync<List<Tarea>>($"{urlBase}Tareas/GetTareaUsuario/{usuarioId}") ?? new();
        }

        public async Task<Tarea> GetTareaById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tarea>($"{urlBase}Tareas/{id}") ?? new();
        }

        public async Task<List<Prioridad>> GetPrioridad()
        {
            return await _httpClient.GetFromJsonAsync<List<Prioridad>>($"{urlBase}Tareas/GetPrioridad") ?? new();
        }

        public async Task MarcarTareaCompletada(Tarea tarea)
        {
            tarea.Finalizado = true;
            await ActualizarTarea(tarea);
        }
    }
}
