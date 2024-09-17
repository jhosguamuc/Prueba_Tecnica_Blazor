using front.Models;
using front.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection;

namespace front.Components.Pages
{
    public partial class Usuarios
    {
        [Inject]
        IUsuarioService? usuarioService { get; set; }
        [Inject]
        NavigationManager? NavigationManager { get; set; }
        [Inject]
        IJSRuntime? JsRuntime { get; set; }

        List<Usuario> usuarios = [];
        Usuario nuevoUsuario = new Usuario();
        private bool isVisible;

        protected override async Task OnInitializedAsync()
        {
            if (usuarioService is not null)
                usuarios = await usuarioService.GetUsuarios();
        }

        async Task CrearUsuario()
        {
            if(usuarioService is not null)
            {
                await usuarioService.CrearUsuario(nuevoUsuario);
                nuevoUsuario = new();
                usuarios = await usuarioService.GetUsuarios();
                Hide();
            }            
        }

        void AsignarTarea(Usuario user)
        {
            if (NavigationManager is not null)
                NavigationManager.NavigateTo($"/tareas/{user.Id}");
        }

        async Task EliminarUsuario(int id)
        {
            if (usuarioService is not null && JsRuntime is not null)
            {
                bool confirmDelete = await JsRuntime.InvokeAsync<bool>("confirm", "¿Estás seguro de que quieres eliminar el usuario?");
                if (!confirmDelete) return;

                await usuarioService.EliminarUsuario(id);
                usuarios = await usuarioService.GetUsuarios();
            }
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
