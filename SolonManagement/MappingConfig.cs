using AutoMapper.Features;
using AutoMapper;
using SalonManagement.Models.Dto;
using SalonManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using System.Drawing;
using SalonManagement.Models;

namespace SalonManagement
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //    CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            //    CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

            //    CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            //    CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, IdentityUser>().ReverseMap();

            CreateMap<ApplicationRole, ApplicationRoleDTO>().ReverseMap();
            CreateMap<ApplicationRole, IdentityRole>().ReverseMap();

            CreateMap<ApplicationUserRole, ApplicationUserRoleDTO>().ReverseMap();
            CreateMap<ApplicationUserRole, IdentityUserRole<string>>().ReverseMap();


            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Payment, PaymentCreateDTO>().ReverseMap();
            CreateMap<Payment, PaymentUpdateDTO>().ReverseMap();

            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<Gender, GenderCreateDTO>().ReverseMap();
            CreateMap<Gender, GenderUpdateDTO>().ReverseMap();

            CreateMap<FirstService, FirstServiceDTO>().ReverseMap();
            CreateMap<FirstService, FirstServiceCreateDTO>().ReverseMap();
            CreateMap<FirstService, FirstServiceUpdateDTO>().ReverseMap();

            CreateMap<SecondService, SecondServiceDTO>().ReverseMap();
            CreateMap<SecondService, SecondServiceCreateDTO>().ReverseMap();
            CreateMap<SecondService, SecondServiceUpdateDTO>().ReverseMap();

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CountryCreateDTO>().ReverseMap();
            CreateMap<Country, CountryUpdateDTO>().ReverseMap();

            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<State, StateCreateDTO>().ReverseMap();
            CreateMap<State, StateUpdateDTO>().ReverseMap();

            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<City, CityCreateDTO>().ReverseMap();
            CreateMap<City, CityUpdateDTO>().ReverseMap();

            CreateMap<SalonBranchXService, SalonBranchXServiceDTO>().ReverseMap();
            CreateMap<SalonBranchXService, SalonBranchXServiceCreateDTO>().ReverseMap();
            CreateMap<SalonBranchXService, SalonBranchXServiceUpdateDTO>().ReverseMap();

            CreateMap<SalonBranchXPayment, SalonBranchXPaymentDTO>().ReverseMap();
            CreateMap<SalonBranchXPayment, SalonBranchXPaymentCreateDTO>().ReverseMap();
            CreateMap<SalonBranchXPayment, SalonBranchXPaymentUpdateDTO>().ReverseMap();

            CreateMap<SalonBranchXGender, SalonBranchXGenderDTO>().ReverseMap();
            CreateMap<SalonBranchXGender, SalonBranchXGenderCreateDTO>().ReverseMap();
            CreateMap<SalonBranchXGender, SalonBranchXGenderUpdateDTO>().ReverseMap();

            CreateMap<Booking, BookingDTO>().ReverseMap();
            CreateMap<Booking, BookingCreateDTO>().ReverseMap();
            CreateMap<Booking, BookingUpdateDTO>().ReverseMap();

            CreateMap<SalonBranch, SalonBranchDTO>().ReverseMap();
            CreateMap<SalonBranch, SalonBranchCreateDTO>().ReverseMap();
            CreateMap<SalonBranch, SalonBranchUpdateDTO>().ReverseMap();
        }
    }
}
