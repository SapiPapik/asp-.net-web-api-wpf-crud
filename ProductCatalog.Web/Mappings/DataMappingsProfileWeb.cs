using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ProductCatalog.BLL.Contracts.Dtos;
using ProductCatalog.Web.Models;

namespace ProductCatalog.Web.Mappings {
    public class DataMappingsProfileWeb : Profile {
        public DataMappingsProfileWeb() {

            #region Brand

            CreateMap<BrandDto, BrandResponseModel>()
                .ForMember(b => b.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(b => b.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(b => b.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion

            #region ToolType

            CreateMap<ToolTypeDto, ToolTypeResponseModel>()
                .ForMember(tt => tt.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(tt => tt.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(tt => tt.DescriptionApplicationOptions, opt => opt.MapFrom(src => src.DescriptionApplicationOptions))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion

            #region Equipment

            CreateMap<EquipmentDto, EquipmentResponseModel>()
                .ForMember(e => e.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(e => e.Specifications, opt => opt.MapFrom(src => src.Specifications))
                .ForMember(e => e.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(e => e.BrandId, opt => opt.MapFrom(src => src.BrandId))
                .ForMember(e => e.ToolTypeId, opt => opt.MapFrom(src => src.ToolTypeId))
                .ForMember(e => e.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(e => e.ToolType, opt => opt.MapFrom(src => src.ToolType))
                .ReverseMap()
                .ForAllOtherMembers(opt => opt.Ignore());

            #endregion
        }
    }
}