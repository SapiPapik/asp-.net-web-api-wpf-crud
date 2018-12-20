using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductCatalog.BLL.Contracts.Dtos;
using ProductCatalog.Data.Entities;

namespace ProductCatalog.BLL.Services.Mappings {
    public class DataMappingsProfile : Profile {
        public DataMappingsProfile() {

            #region Brand

            CreateMap<Brand, BrandDto>()
                .ForMember(b => b.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(b => b.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(b => b.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion

            #region Equipment

            CreateMap<Equipment, EquipmentDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(e => e.Specifications, opt => opt.MapFrom(src => src.Specifications))
                .ForMember(e => e.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(e => e.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(e => e.ToolTypeId, opt => opt.MapFrom(src => src.ToolTypeId))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion

            #region ToolType

            CreateMap<ToolType, ToolTypeDto>()
                .ForMember(tt => tt.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(tt => tt.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(tt => tt.DescriptionApplicationOptions, opt => opt.MapFrom(src => src.DescriptionApplicationOptions))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion
        }
    }
}
