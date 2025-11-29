
using gestion_biblioteca_api.Data;
using gestion_biblioteca_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestion_biblioteca_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosCategoriaController : ControllerBase
    {

        private readonly BibliotecaContext _context;
        
        public LibrosCategoriaController(BibliotecaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<LibroCategoria>> PostLibroCategoria(LibroCategoria libroCategoria)
        {
            _context.LibroCategorias.Add(libroCategoria);
            await _context.SaveChangesAsync();
            return Ok(libroCategoria);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroCategoria>>> GetLibrosCategoria()
        {
            return await _context.LibroCategorias.ToListAsync();
        }

    }
}