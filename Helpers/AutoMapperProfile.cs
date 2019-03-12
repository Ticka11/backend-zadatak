using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BackEnd_zadatak.Dtos;
using BackEnd_zadatak.Dtos.DeviceTypeDtos;
using BackEnd_zadatak.Models;

namespace BackEnd_zadatak.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // mapiranje uredjaja
            CreateMap<DeviceForCreationOrUpdate, Device>();
           
            CreateMap<Device, DeviceToReturnDto>()
                .ForMember(dest => dest.DeviceType, opt => {
                    opt.MapFrom(src => src.DeviceType.Name);
                });

            //mapiranje vrijednosti osobina uredjaja

            CreateMap<DevicePropertyForCreationOrUpdate, DevicePropertyValue>();
            CreateMap<DevicePropertyValue, DevicePropertyToReturnDto>()
                .ForMember(dest => dest.DeviceTypeProperty, opt => {
                    opt.MapFrom(src => src.DeviceTypeProperty.Name);
                });

            //mapiranje tipova uredjaja
            CreateMap<DeviceTypeToCreateOrUpdate, DeviceType>()
                .ForMember(dest => dest.DeviceTypeProperty, opt => {
                    opt.MapFrom(src => src.DeviceTypeProperties);
                });
            CreateMap<DeviceType, DeviceTypeToReturnListDto>()
                .ForMember(dest => dest.ParentTypeProperties, opt => {
                    opt.MapFrom(src => src.ParentDeviceType.DeviceTypeProperty);
                })
                .ForMember(dest => dest.DeviceTypeProperties, opt => {
                    opt.MapFrom(src => src.DeviceTypeProperty);
                });
             
            CreateMap<DeviceType, DeviceTypeToReturnSingleDto>()
                .ForMember(dest => dest.ParentTypeProperties, opt => {
                    opt.MapFrom(src => src.ParentDeviceType.DeviceTypeProperty);
                })
                .ForMember(dest => dest.ParentDeviceType, opt => {
                    opt.MapFrom(src => src.ParentDeviceType.Name);
                })
                 .ForMember(dest => dest.DeviceTypeProperties, opt => {
                    opt.MapFrom(src => src.DeviceTypeProperty);
                });

            //mapiranje osobina tipova uredjaja    
                
            CreateMap<TypePropertyForCreationOrUpdate, DeviceTypeProperty>();
            CreateMap<DeviceTypeProperty, TypePropertyToReturnDto>();

        }
    }
}