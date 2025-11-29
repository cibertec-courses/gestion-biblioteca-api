

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_biblioteca_api.Models
{
    [Table("prestamos")]
    public class Prestamo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("libro_id")]
        public int LibroId { get; set; }


        [Required(ErrorMessage ="El nombre de usuario es obligatorio")]
        [StringLength(100)]
        [Column("nombre_usaurio")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Column("fecha_prestamo")]
        public DateTime FechaPrestamo { get; set; }= DateTime.Now;

        [Column("fecha_devolucion")]
        public DateTime? FechaDevolucion { get; set; }

        [Column("devuelto")]
        public bool Devuelto { get; set; }= false;

        [ForeignKey("LibroId")]
        public Libro? Libro{ get; set; } = null!;

    }
}