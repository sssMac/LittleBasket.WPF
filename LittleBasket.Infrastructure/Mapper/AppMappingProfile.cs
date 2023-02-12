using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Infrastructure.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Domain.Entities.Product, Domain.Models.Product>();
            CreateMap<Domain.Entities.History, Domain.Models.History>();
            CreateMap<Domain.Entities.Check, Domain.Models.Check>();
        }
    }
}
