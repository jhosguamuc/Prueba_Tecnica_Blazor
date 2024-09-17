namespace front.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Finalizado { get; set; }
        public bool Eliminado { get; set; }
        public string Tags { get; set; }
        public int IdPrioridad { get; set; }
    }

}
