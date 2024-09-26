using AutoMapper;
using DoubleV.DTOs;
using DoubleV;
using DoubleV.Modelos;

namespace DoubleV.Mapping
{
    public class MappingTarea : AutoMapper.Profile
    {
        public MappingTarea()
        {
            CreateMap<TareaSinIdDTO, Tarea>()
                .ForMember(dest => dest.Usuario, opt => opt.Ignore());

            CreateMap<Tarea, TareaSinIdDTO>();

            CreateMap<TareaActualizarDTO, Tarea>()
            .ForMember(dest => dest.TareaId, opt => opt.Ignore()) // Ignorar TareaId para no cambiar el ID
            .ForMember(dest => dest.Usuario, opt => opt.Ignore());

            CreateMap<Tarea, TareaConUsuarioDTO>()
            .ForMember(dest => dest.UsuarioNombre, opt => opt.MapFrom(src => src.Usuario.Nombre)) 
            .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId));
        }        
    }
}
