using AutoMapper;
using Entities.Models;
using Entities.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper.AutoMapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping() {
            CreateMap<BaseValues, PersonalDataDto>();
            CreateMap<PersonalDataDto, BaseValues>();  
            CreateMap<Personal, PersonalDataDto>();
            CreateMap<PersonalDataDto, Personal>();
        }
        

    }
}
