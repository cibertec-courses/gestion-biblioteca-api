
using AutoMapper;
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
        private readonly IMapper _mapper;

        public LibrosController(BibliotecaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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


            var librosDTO = _mapper.Map<List<LibroDTO>>(libros);
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

            var LibroDTO = _mapper.Map<LibroDTO>(libro);

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