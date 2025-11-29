
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gestion_biblioteca_api.Models
{
    [Table("libros")]
    public class Libro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage ="El titulo es obligatorio")]
        [StringLength(200)]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage ="El ISBN es obligatorio")]
        [StringLength(13)]
        [Column("isbn")]
        public string ISBN { get; set; } = string.Empty;

        [Column("fecha_publicacion")]
        public DateTime? FechaPublicacion { get; set; }

        [Column("numero_paginas")]
        public int NumeroPaginas { get; set; }

        [Column("diponible")]
        public bool Disponible { get; set; } = true;

        [Column("autor_id")]
        public int AutorId { get; set; }

        [ForeignKey("AutorId")]
        public Autor? Autor { get; set; } = null!;

        public ICollection<LibroCategoria>? LibroCategorias { get; set; } = new List<LibroCategoria>();

        public ICollection<Prestamo>? Prestamos { get; set; } = new List<Prestamo>();

    }
}