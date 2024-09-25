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
            CreateMap<TareaDTO, Tarea>()
                .ForMember(dest => dest.Usuario, opt => opt.Ignore());

            CreateMap<Tarea, TareaDTO>();
        }        
    }
}
