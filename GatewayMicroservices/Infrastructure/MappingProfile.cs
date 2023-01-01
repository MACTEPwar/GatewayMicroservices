using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DAL = GatewayMicroservices.Models.DAL;

namespace GatewayMicroservices.Infrastructure
{
    public class MappingProfile : Profile
    {

        /// <summary>
        /// Current profile for using in AutoMapper
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Route, DAL.Route>().ReverseMap();
        }
    }
}
