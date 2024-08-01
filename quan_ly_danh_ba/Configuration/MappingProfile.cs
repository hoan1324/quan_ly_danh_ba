using AutoMapper;
using Data.Entity;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Configuration
{

    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

             CreateMap<Contact, ContactCreateDto>()
            .ForMember(des => des.GroupNames,
             opt => opt.MapFrom(src => src.GroupContacts.Select(gc => gc.GroupName).ToList())
             ).ReverseMap();

            CreateMap<ContactCreateDto, Contact>()
            .ForMember(des => des.User, opt => opt.Ignore());

            CreateMap<GroupContactDto,GroupContact>()
            .ForMember(des=>des.Contacts,opt=>opt.Ignore())
            .ForMember(des=>des.User, opt=>opt.Ignore())
            .ReverseMap();

            
            
            CreateMap<UserDto, User>()
            .ForMember(des=>des.Contacts, opt=>opt.Ignore())
            .ForMember(des=>des.GroupContacts, opt =>opt.Ignore())
            .ReverseMap();


        }
    }

}
