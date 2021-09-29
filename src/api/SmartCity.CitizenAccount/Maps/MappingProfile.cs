using AutoMapper;
using SmartCity.CitizenAccount.Api.Models.Citizens;
using SmartCity.CitizenAccount.Api.Models.Emails;
using SmartCity.CitizenAccount.Api.Models.Payment;
using SmartCity.CitizenAccount.Api.Models.Users;
using SmartCity.CitizenAccount.Data.Access.Common;
using SmartCity.CitizenAccount.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.CitizenAccount.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CitizenMappings();
            UserMappings();
            EmailMappings();
            InvoiceMappings();
        }

        private void CitizenMappings()
        {
            CreateMap<Citizen, CitizenModel>();
            CreateMap<CreateCitizenModel, Citizen>()
                .ForMember(dst => dst.Gender, opt => opt.MapFrom(src => (Gender)Enum.Parse(typeof(Gender), src.Gender.Capitalize())))
                .ForMember(dst => dst.DateOfBirth, opt => opt.MapFrom(src =>
                    DateTime.ParseExact(src.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }

        private void UserMappings()
        {
            // User
            CreateMap<User, UserModel>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status.ToDescriptionString()));
            CreateMap<CreateUserModel, User>().AfterMap<TrimAllStringProperties>();
            CreateMap<UserModel, UpdateUserModel>();
            CreateMap<UpdateUserModel, User>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => (Status)Enum.Parse(typeof(Status), src.Status.Capitalize())));

            // VerificationRequest
            CreateMap<UpdateVerificationRequestModel, VerificationRequest>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => (VerificationStatus)Enum.Parse(typeof(VerificationStatus), src.Status.Capitalize())));
            CreateMap<VerificationStatus, VerificationStatusModel>()
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.ToDescriptionString()));
            CreateMap<CreateVerificationRequest, VerificationRequest>();
            CreateMap<VerificationRequest, VerificationRequestModel>()
                .ForMember(dst => dst.UserData, opt => opt.MapFrom(src => src.User))
                .ForMember(dst => dst.CitizenData, opt => opt.MapFrom(src => src.Citizen))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status.ToDescriptionString()));

            // Auth
            CreateMap<UserWithToken, UserWithTokenModel>();
        }

        private void InvoiceMappings()
        {
            CreateMap<MakePaymentModel, Invoice>()
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => (InvoiceCategory)Enum.Parse(typeof(InvoiceCategory), src.Category.Capitalize())));

            CreateMap<Invoice, InvoiceModel>()
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category.ToDescriptionString()))
                .ForMember(dst => dst.InvoiceType, opt => opt.MapFrom(src => src.InvoiceType.ToDescriptionString()));
        }

        private void EmailMappings()
        {
            CreateMap<Email, EmailModel>();
            CreateMap<CreateEmailModel, Email>();
        }
    }

    internal class PrepareEmails : IMappingAction<object, object>
    {
        public void Process(object source, object destination, ResolutionContext context)
        {
            
        }
    }

    internal class TrimAllStringProperties : IMappingAction<object, object>
    {
        public void Process(object source, object destination, ResolutionContext context)
        {
            var stringProperties = destination.GetType().GetProperties().Where(p => p.PropertyType == typeof(string));
            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(destination, null);
                if (currentValue != null)
                {
                    stringProperty.SetValue(destination, currentValue.Trim(), null);
                }
            }
        }
    }

    internal static class EnumExtensions
    {
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    internal static class StringExtensions
    {
        public static string Capitalize(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input[0].ToString().ToUpper() + input.Substring(1)
        };
    }
}
