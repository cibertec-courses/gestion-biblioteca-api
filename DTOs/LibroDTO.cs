
namespace gestion_biblioteca_api.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public DateTime? FechaPublicacion { get; set; }
        public int? NumeroPaginas { get; set; }
        public bool Disponible { get; set; }
        public string Autor {get; set;} = string.Empty;
        public List<string> Categorias { get; set; } = new List<string>();

    }
}