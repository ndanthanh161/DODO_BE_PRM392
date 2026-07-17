using AutoMapper;
using SMEFLOWSystem.Application.DTOs.RoleDtos;
using SMEFLOWSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.Mappings
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleUpdatedDto>();
            CreateMap<Role, RoleDto>();

            CreateMap<RoleDto, Role>();
            CreateMap<RoleUpdatedDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.IsSystemRole,
                           opt => opt.MapFrom(src => (bool?)src.IsSystemRole));
        }

    }
}
