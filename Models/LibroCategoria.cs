
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_biblioteca_api.Models
{
    [Table("libro_categorias")]
    public class LibroCategoria
    {
        [Column("libro_id")]
        public int LibroId { get; set; }

        [Column("categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("LibroId")]
        public Libro Libro { get; set; } = null!;

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } = null!;


        
    }
}