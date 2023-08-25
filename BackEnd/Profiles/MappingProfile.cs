using AutoMapper;
using DTO;
using Domains;
using DTO.Combos;
using DTO.Asesor;
using DTO.AutoNuevo;
using DTO.AutosUsados;
using DTO.Citas;
using DTO.Usuarios;

namespace BackEnd.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Horario, HorarioComboDTO>();
            CreateMap<Asesor, AsesorComboDTO>();
            CreateMap<Rol, RolComboDTO>();


            CreateMap<Asesor, AsesorGetPageDTO>().ReverseMap();
            CreateMap<Asesor, AsesorGetAdminDTO>();
            CreateMap<AsesorPutDTO, Asesor>();


            CreateMap<Auto, NuevoGetDTO>();
            CreateMap<NuevoPostDTO, Auto>();
            CreateMap<NuevoPostDTO, Nuevo>();
            CreateMap<NuevoPutDTO, Auto>();


            CreateMap<Auto, UsadoGetDTO>();
            CreateMap<UsadoPostDTO, Auto>();
            CreateMap<UsadoPostDTO, Usado>();


            CreateMap<Cita, CitaGetDTO>();
            CreateMap<CitaPostDTO, Cita>();


            CreateMap<Usuario, UsuarioGetDTO>();
            CreateMap<UsuarioPostDTO, Usuario>();
        }
    }
}
