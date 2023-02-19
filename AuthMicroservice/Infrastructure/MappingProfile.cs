using System.Linq;
using AuthMicroservice.Models.DAL;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Infrastructure
{
    public class MappingProfile : Profile
    {

        /// <summary>
        /// Current profile for using in AutoMapper
        /// </summary>
        public MappingProfile()
        {
            //CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
