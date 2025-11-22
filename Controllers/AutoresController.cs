
using gestion_biblioteca_api.Data;
using gestion_biblioteca_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gestion_biblioteca_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresContoller : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public AutoresContoller(BibliotecaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            return await _context.Autores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Autor>> GetAutor(int id)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);
            return autor == null? NotFound() : autor;
        }

    }
}