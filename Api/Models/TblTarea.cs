namespace Api.Models
{
    public class TblTarea
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public TblUsuario? Usuario { get; set; }  // Relación con Usuario
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime FechaVencimiento { get; set; }
        public bool Finalizado { get; set; }
        public bool Eliminado { get; set; }
        public string Tags { get; set; } = null!;      
        public DateTime? Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
        public int IdPrioridad { get; set; }
        public TblPrioridad? Prioridad { get; set; }  // Relación con Prioridad
    }

}
