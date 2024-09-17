using front.Models;
using front.Services;
using front.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading;

namespace front.Components.Pages
{
    public partial class Tareas
    {
        [Inject]
        ITareaService? TareaService { get; set; }
        [Inject]
        NavigationManager? NavigationManager { get; set; }
        [Inject]
        IJSRuntime? JsRuntime { get; set; }

        //Parametros url
        [Parameter]
        public int UsuarioId { get; set; }

        List<Tarea> tareas = new();
        List<Prioridad> prioridades = new();
        Tarea nuevaTarea = new();
        bool isnew = true;
        private bool isVisible;

        protected override async Task OnInitializedAsync()
        {
            await CargarTareas();
        }

        async Task CargarTareas()
        {
            if (TareaService is not null)
            {
                tareas = await TareaService.GetTareas(UsuarioId);
                tareas = tareas.OrderBy(t => t.FechaVencimiento).ToList();
                prioridades = await TareaService.GetPrioridad();
            }
            nuevaTarea.FechaVencimiento = DateTime.Now;
        }

        async Task FindTarea(int id)
        {
            if (TareaService is not null)
            {
                nuevaTarea = await TareaService.GetTareaById(id);
                isnew = false;
                Show();
            }
        }

        async Task CrearTarea()
        {
            if (TareaService is not null)
            {
                nuevaTarea.IdUsuario = UsuarioId;
                await TareaService.CrearTarea(nuevaTarea);
                nuevaTarea = new Tarea();
                await CargarTareas();
                Cancel();
            }
        }

        async Task ModificarTarea()
        {
            if (TareaService is not null)
            {
                nuevaTarea.IdUsuario = UsuarioId;
                await TareaService.ActualizarTarea(nuevaTarea);
                nuevaTarea = new Tarea();
                await CargarTareas();
                Cancel();
            }
        }        

        async Task EliminarTarea(int id)
        {
            if (TareaService is not null && JsRuntime is not null)
            {

                bool confirmDelete = await JsRuntime.InvokeAsync<bool>("confirm", "¿Estás seguro de que quieres eliminar esta tarea?");
                if (!confirmDelete) return;

                await TareaService.EliminarTarea(id);
                await CargarTareas();
            }
        }

        async Task MarcarCompletada(Tarea tarea)
        {
            if (TareaService is not null)
            {
                await TareaService.MarcarTareaCompletada(tarea);
                await CargarTareas(); 
            }                
        }

        void Cancel()
        {
            isnew = true;
            nuevaTarea = new();
            nuevaTarea.FechaVencimiento = DateTime.Now;
            Hide();
        }

        public void Show()
        {
            isVisible = true;
            StateHasChanged(); // Actualiza la UI
        }

        public void Hide()
        {
            isVisible = false;
            StateHasChanged(); // Actualiza la UI
        }
    }
}
