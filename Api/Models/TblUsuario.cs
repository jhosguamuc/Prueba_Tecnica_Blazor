namespace Api.Models
{
    public class TblUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }

}
