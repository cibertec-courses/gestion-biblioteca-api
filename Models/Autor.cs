using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestor_biblioteca_api.Models
{
    [Table("autores")]
    public class Autor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del autor es obligatorio")]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100)]
        [Column("apellido")]
        public string Apellido { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("nacionalidad")]
        public string? Nacionalidad { get; set; }

        [Column("fecha_nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        // Libro
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();

    }
}