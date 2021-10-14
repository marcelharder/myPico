using System;
using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using myPicoAPI.Dtos;
using myPicoAPI.Models;

namespace DatingApp.API.Helpers {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles () {
            CreateMap<User, UserForListDto> ()
                .ForMember (dest => dest.PhotoUrl, opt => { opt.MapFrom (src => src.Photos.FirstOrDefault (p => p.IsMain).Url); })
                .ForMember (dest => dest.Age, opt => { opt.MapFrom (d => d.DateOfBirth.CalculateAge ()); });

            CreateMap<User, UserForDetailedDto> ()
                .ForMember (dest => dest.PhotoUrl, opt => { opt.MapFrom (src => src.Photos.FirstOrDefault (p => p.IsMain).Url); })
                .ForMember (dest => dest.Age, opt => { opt.MapFrom (d => d.DateOfBirth.CalculateAge ()); });

            CreateMap<Photo, PhotosForDetailedDto> ();
            CreateMap<UserForUpdateDto, User> ().ForMember (dest => dest.UserId, opt => opt.Ignore());
            CreateMap<PhotoForCreationDto, Photo> ();
            CreateMap<Photo, PhotoForReturnDto> ();
            CreateMap<UserForRegisterDto, User> ();
            CreateMap<MessageForCreationDto, Message> ().ReverseMap ();

            CreateMap<Appointment, AppointmentForReturnDto> ();
                // .ForMember (dest => dest.Status, opt => { opt.MapFrom (d => d.Status.ChangeStatus ()); });
                // .ForMember (dest => dest.RentUSD, opt => { opt.MapFrom (d => d.Rent.CalculateUSDFromPHP ()); });

            CreateMap<AppointmentForUpdateDto, Appointment> ();

            CreateMap<AppointmentForCreationDto, Appointment> ()
               .ForMember (dest => dest.NoOfNights, opt => { opt.MapFrom (d => d.RequestedDays.CalculateNumberOfDays ()); })
               .ForMember (dest => dest.UnitId, opt => opt.MapFrom (u => u.picoUnitId));

            CreateMap<Message, MessageToReturnDto> ()
                .ForMember (m => m.SenderPhotoUrl, opt => opt.MapFrom (u => u.Sender.Photos.FirstOrDefault (p => p.IsMain).Url))
                .ForMember (m => m.RecipientPhotoUrl, opt => opt.MapFrom (u => u.Recipient.Photos.FirstOrDefault (p => p.IsMain).Url));

            CreateMap<dateOccupancy, SeasonForReturnDTO> ();
            
            CreateMap<SeasonForReturnDTO, dateOccupancy> ().ForMember (dest => dest.Id, opt => opt.Ignore());
            

        }

        private void CreateMap<T> (Message message) {
            throw new NotImplementedException ();
        }
    }
}