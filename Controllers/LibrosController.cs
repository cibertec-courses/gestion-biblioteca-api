
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
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> GetLibro(int id)
        {
            var libro = await _context.Libros
                                        .Include(l => l.Autor)
                                        .Include(l => l.LibroCategorias)
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
                Autor = $"{libro.Autor.Nombre} {libro.Autor.Apellido}",
                Categorias = libro.LibroCategorias.Select(lc => lc.Categoria.Nombre).ToList()
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