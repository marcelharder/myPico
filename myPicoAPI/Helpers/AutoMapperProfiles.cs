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

            CreateMap<dateOccupancy, SeasonForReturnDTO> ()
                .ForMember (m => m.day_1, opt => { opt.MapFrom (d => d.day_1.getSeasonDescription ()); })
                .ForMember (m => m.day_2, opt => { opt.MapFrom (d => d.day_2.getSeasonDescription ()); })
                .ForMember (m => m.day_3, opt => { opt.MapFrom (d => d.day_3.getSeasonDescription ()); })
                .ForMember (m => m.day_4, opt => { opt.MapFrom (d => d.day_4.getSeasonDescription ()); })
                .ForMember (m => m.day_5, opt => { opt.MapFrom (d => d.day_5.getSeasonDescription ()); })
                .ForMember (m => m.day_6, opt => { opt.MapFrom (d => d.day_6.getSeasonDescription ()); })
                .ForMember (m => m.day_7, opt => { opt.MapFrom (d => d.day_7.getSeasonDescription ()); })
                .ForMember (m => m.day_8, opt => { opt.MapFrom (d => d.day_8.getSeasonDescription ()); })
                .ForMember (m => m.day_9, opt => { opt.MapFrom (d => d.day_9.getSeasonDescription ()); })
                .ForMember (m => m.day_10, opt => { opt.MapFrom (d => d.day_10.getSeasonDescription ()); })
                .ForMember (m => m.day_11, opt => { opt.MapFrom (d => d.day_11.getSeasonDescription ()); })
                .ForMember (m => m.day_12, opt => { opt.MapFrom (d => d.day_12.getSeasonDescription ()); })
                .ForMember (m => m.day_13, opt => { opt.MapFrom (d => d.day_13.getSeasonDescription ()); })
                .ForMember (m => m.day_14, opt => { opt.MapFrom (d => d.day_14.getSeasonDescription ()); })
                .ForMember (m => m.day_15, opt => { opt.MapFrom (d => d.day_15.getSeasonDescription ()); })
                .ForMember (m => m.day_16, opt => { opt.MapFrom (d => d.day_16.getSeasonDescription ()); })
                .ForMember (m => m.day_17, opt => { opt.MapFrom (d => d.day_17.getSeasonDescription ()); })
                .ForMember (m => m.day_18, opt => { opt.MapFrom (d => d.day_18.getSeasonDescription ()); })
                .ForMember (m => m.day_19, opt => { opt.MapFrom (d => d.day_19.getSeasonDescription ()); })
                .ForMember (m => m.day_20, opt => { opt.MapFrom (d => d.day_20.getSeasonDescription ()); })
                .ForMember (m => m.day_21, opt => { opt.MapFrom (d => d.day_21.getSeasonDescription ()); })
                .ForMember (m => m.day_22, opt => { opt.MapFrom (d => d.day_22.getSeasonDescription ()); })
                .ForMember (m => m.day_23, opt => { opt.MapFrom (d => d.day_23.getSeasonDescription ()); })
                .ForMember (m => m.day_24, opt => { opt.MapFrom (d => d.day_24.getSeasonDescription ()); })
                .ForMember (m => m.day_25, opt => { opt.MapFrom (d => d.day_25.getSeasonDescription ()); })
                .ForMember (m => m.day_26, opt => { opt.MapFrom (d => d.day_26.getSeasonDescription ()); })
                .ForMember (m => m.day_27, opt => { opt.MapFrom (d => d.day_27.getSeasonDescription ()); })
                .ForMember (m => m.day_28, opt => { opt.MapFrom (d => d.day_28.getSeasonDescription ()); })
                .ForMember (m => m.day_29, opt => { opt.MapFrom (d => d.day_29.getSeasonDescription ()); })
                .ForMember (m => m.day_30, opt => { opt.MapFrom (d => d.day_30.getSeasonDescription ()); })
                .ForMember (m => m.day_31, opt => { opt.MapFrom (d => d.day_31.getSeasonDescription ()); })
                .ForMember (m => m.day_32, opt => { opt.MapFrom (d => d.day_32.getSeasonDescription ()); })
                .ForMember (m => m.day_33, opt => { opt.MapFrom (d => d.day_33.getSeasonDescription ()); })
                .ForMember (m => m.day_34, opt => { opt.MapFrom (d => d.day_34.getSeasonDescription ()); })
                .ForMember (m => m.day_35, opt => { opt.MapFrom (d => d.day_35.getSeasonDescription ()); })
                .ForMember (m => m.day_36, opt => { opt.MapFrom (d => d.day_36.getSeasonDescription ()); })
                .ForMember (m => m.day_37, opt => { opt.MapFrom (d => d.day_37.getSeasonDescription ()); })
                .ForMember (m => m.day_38, opt => { opt.MapFrom (d => d.day_38.getSeasonDescription ()); })
                .ForMember (m => m.day_39, opt => { opt.MapFrom (d => d.day_39.getSeasonDescription ()); })
                .ForMember (m => m.day_40, opt => { opt.MapFrom (d => d.day_40.getSeasonDescription ()); })
                .ForMember (m => m.day_41, opt => { opt.MapFrom (d => d.day_41.getSeasonDescription ()); })
                .ForMember (m => m.day_42, opt => { opt.MapFrom (d => d.day_42.getSeasonDescription ()); });

            CreateMap<SeasonForReturnDTO, dateOccupancy> ()
                .ForMember (m => m.day_1, opt => { opt.MapFrom (d => d.day_1.postSeasonDescription ()); })
                .ForMember (m => m.day_2, opt => { opt.MapFrom (d => d.day_2.postSeasonDescription ()); })
                .ForMember (m => m.day_3, opt => { opt.MapFrom (d => d.day_3.postSeasonDescription ()); })
                .ForMember (m => m.day_4, opt => { opt.MapFrom (d => d.day_4.postSeasonDescription ()); })
                .ForMember (m => m.day_5, opt => { opt.MapFrom (d => d.day_5.postSeasonDescription ()); })
                .ForMember (m => m.day_6, opt => { opt.MapFrom (d => d.day_6.postSeasonDescription ()); })
                .ForMember (m => m.day_7, opt => { opt.MapFrom (d => d.day_7.postSeasonDescription ()); })
                .ForMember (m => m.day_8, opt => { opt.MapFrom (d => d.day_8.postSeasonDescription ()); })
                .ForMember (m => m.day_9, opt => { opt.MapFrom (d => d.day_9.postSeasonDescription ()); })
                .ForMember (m => m.day_10, opt => { opt.MapFrom (d => d.day_10.postSeasonDescription ()); })
                .ForMember (m => m.day_11, opt => { opt.MapFrom (d => d.day_11.postSeasonDescription ()); })
                .ForMember (m => m.day_12, opt => { opt.MapFrom (d => d.day_12.postSeasonDescription ()); })
                .ForMember (m => m.day_13, opt => { opt.MapFrom (d => d.day_13.postSeasonDescription ()); })
                .ForMember (m => m.day_14, opt => { opt.MapFrom (d => d.day_14.postSeasonDescription ()); })
                .ForMember (m => m.day_15, opt => { opt.MapFrom (d => d.day_15.postSeasonDescription ()); })
                .ForMember (m => m.day_16, opt => { opt.MapFrom (d => d.day_16.postSeasonDescription ()); })
                .ForMember (m => m.day_17, opt => { opt.MapFrom (d => d.day_17.postSeasonDescription ()); })
                .ForMember (m => m.day_18, opt => { opt.MapFrom (d => d.day_18.postSeasonDescription ()); })
                .ForMember (m => m.day_19, opt => { opt.MapFrom (d => d.day_19.postSeasonDescription ()); })
                .ForMember (m => m.day_20, opt => { opt.MapFrom (d => d.day_20.postSeasonDescription ()); })
                .ForMember (m => m.day_21, opt => { opt.MapFrom (d => d.day_21.postSeasonDescription ()); })
                .ForMember (m => m.day_22, opt => { opt.MapFrom (d => d.day_22.postSeasonDescription ()); })
                .ForMember (m => m.day_23, opt => { opt.MapFrom (d => d.day_23.postSeasonDescription ()); })
                .ForMember (m => m.day_24, opt => { opt.MapFrom (d => d.day_24.postSeasonDescription ()); })
                .ForMember (m => m.day_25, opt => { opt.MapFrom (d => d.day_25.postSeasonDescription ()); })
                .ForMember (m => m.day_26, opt => { opt.MapFrom (d => d.day_26.postSeasonDescription ()); })
                .ForMember (m => m.day_27, opt => { opt.MapFrom (d => d.day_27.postSeasonDescription ()); })
                .ForMember (m => m.day_28, opt => { opt.MapFrom (d => d.day_28.postSeasonDescription ()); })
                .ForMember (m => m.day_29, opt => { opt.MapFrom (d => d.day_29.postSeasonDescription ()); })
                .ForMember (m => m.day_30, opt => { opt.MapFrom (d => d.day_30.postSeasonDescription ()); })
                .ForMember (m => m.day_31, opt => { opt.MapFrom (d => d.day_31.postSeasonDescription ()); })
                .ForMember (m => m.day_32, opt => { opt.MapFrom (d => d.day_32.postSeasonDescription ()); })
                .ForMember (m => m.day_33, opt => { opt.MapFrom (d => d.day_33.postSeasonDescription ()); })
                .ForMember (m => m.day_34, opt => { opt.MapFrom (d => d.day_34.postSeasonDescription ()); })
                .ForMember (m => m.day_35, opt => { opt.MapFrom (d => d.day_35.postSeasonDescription ()); })
                .ForMember (m => m.day_36, opt => { opt.MapFrom (d => d.day_36.postSeasonDescription ()); })
                .ForMember (m => m.day_37, opt => { opt.MapFrom (d => d.day_37.postSeasonDescription ()); })
                .ForMember (m => m.day_38, opt => { opt.MapFrom (d => d.day_38.postSeasonDescription ()); })
                .ForMember (m => m.day_39, opt => { opt.MapFrom (d => d.day_39.postSeasonDescription ()); })
                .ForMember (m => m.day_40, opt => { opt.MapFrom (d => d.day_40.postSeasonDescription ()); })
                .ForMember (m => m.day_41, opt => { opt.MapFrom (d => d.day_41.postSeasonDescription ()); })
                .ForMember (m => m.day_42, opt => { opt.MapFrom (d => d.day_42.postSeasonDescription ()); });

        }

        private void CreateMap<T> (Message message) {
            throw new NotImplementedException ();
        }
    }
}