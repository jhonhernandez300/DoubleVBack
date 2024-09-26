using DoubleV.DTOs;
using DoubleV.Modelos;

namespace DoubleV.Mapping
{
    public class MappingUsuario : AutoMapper.Profile
    {
        public MappingUsuario()
        {
            CreateMap<Usuario, UsuarioSinIdDTO>()
             .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
             .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RolId))
             .ReverseMap();

            CreateMap<LoginDTO, Usuario>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Correo))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Nombre, opt => opt.Ignore()) 
            .ForMember(dest => dest.RolId, opt => opt.Ignore());
        }
    }
}
