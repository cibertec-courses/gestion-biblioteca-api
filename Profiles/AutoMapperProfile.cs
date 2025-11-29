using AutoMapper;
using gestion_biblioteca_api.DTOs;
using gestion_biblioteca_api.Models;

namespace gestion_biblioteca_api.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Libro, LibroDTO>()
                .ForMember(dest => dest.Autor, opt => opt.MapFrom(
                                                        scr => scr.Autor != null ? $"{scr.Autor.Nombre} {scr.Autor.Apellido}" : "Sin Autor"))
                .ForMember(dest => dest.Categorias, opt => opt.MapFrom(
                                                        scr => scr.LibroCategorias != null ?
                                                                    scr.LibroCategorias.Select(lc => lc.Categoria!.Nombre).ToList() : new List<string>()));
        }
        
    }
}