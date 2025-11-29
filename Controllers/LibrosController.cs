
using gestion_biblioteca_api.Data;
using gestion_biblioteca_api.DTOs;
using gestion_biblioteca_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestion_biblioteca_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> GetLibros(
            [FromQuery] bool? disponible,
            [FromQuery] int? autorId,
            [FromQuery] string? buscar,
            [FromQuery] int pagina = 1,
            [FromQuery] int cantidad = 10
        )
        {

            var query = _context.Libros
                        .Include(l => l.Autor)
                        .Include(l => l.LibroCategorias)!
                            .ThenInclude(lc => lc.Categoria)
                        .AsQueryable();

            if (disponible.HasValue)
            {
                query = query.Where(l => l.Disponible == disponible.Value);
            }

            if (autorId.HasValue)
            {
                query = query.Where(l => l.AutorId == autorId.Value);
            }

            if (!string.IsNullOrEmpty(buscar))
            {
                query = query.Where(l => l.Titulo.Contains(buscar));
            }


            var libros = await query
                .Skip((pagina - 1) * cantidad)
                .Take(cantidad)
                .ToListAsync();


            var librosDTO = libros.Select(l => new LibroDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                ISBN = l.ISBN,
                FechaPublicacion = l.FechaPublicacion,
                NumeroPaginas = l.NumeroPaginas,
                Disponible = l.Disponible,
                Autor = l.Autor != null ? $"{l.Autor.Nombre} {l.Autor.Apellido}" : "Sin Autor",
                Categorias = l.LibroCategorias != null ? l.LibroCategorias.Select(lc => lc.Categoria!.Nombre).ToList() : new List<string>()
            }).ToList();
            return librosDTO;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> GetLibro(int id)
        {
            var libro = await _context.Libros
                        .Include(l => l.Autor)!
                        .Include(l => l.LibroCategorias)!
                            .ThenInclude(lc => lc.Categoria)
                        .FirstOrDefaultAsync(l => l.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            var LibroDTO = new LibroDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                ISBN = libro.ISBN,
                FechaPublicacion = libro.FechaPublicacion,
                NumeroPaginas = libro.NumeroPaginas,
                Disponible = libro.Disponible,
                Autor = libro.Autor != null ? $"{libro.Autor!.Nombre} {libro.Autor.Apellido}" : "Sin Autor",
                Categorias = libro.LibroCategorias?.Where(lc => lc.Categoria != null).Select(lc => lc.Categoria!.Nombre).ToList() ?? new List<string>()
            };

            return LibroDTO;
        }

        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
        }

    }
}