using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDeleteDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByIdDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByRequestNumberDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetListByCorporateIdDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetListDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestUpdateDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class PaymentRequestMappingProfile: Profile
    {
        public PaymentRequestMappingProfile()
        {
            CreateMap<PaymentRequest, PaymentRequestAddRequestDto>();
            CreateMap<PaymentRequestAddRequestDto, PaymentRequest>();

            CreateMap<PaymentRequest, PaymentRequestGetByIdResponseDto>();
            CreateMap<PaymentRequestGetByIdResponseDto, PaymentRequest>();

            CreateMap<PaymentRequest, PaymentRequestGetListResponseDto>();
            CreateMap<PaymentRequestGetListResponseDto, PaymentRequest>();

            CreateMap<PaymentRequest, PaymentRequestUpdateRequestDto>();
            CreateMap<PaymentRequestUpdateRequestDto, PaymentRequest>();

            CreateMap<PaymentRequest, PaymentRequestDeleteRequestDto>();
            CreateMap<PaymentRequestDeleteRequestDto, PaymentRequest>();

            CreateMap<PaymentRequest, PaymentRequestGetByTodayResponseDto>();
            CreateMap<PaymentRequestGetByTodayResponseDto, PaymentRequest>();

            CreateMap<PaymentRequest, PaymentRequestGetByRequestNumberResponseDto>();
            CreateMap<PaymentRequestGetByRequestNumberResponseDto, PaymentRequest>();

            CreateMap<PaymentRequest, PaymentRequestGetListByCorporateIdResponseDto>();
            CreateMap<PaymentRequestGetListByCorporateIdResponseDto, PaymentRequest>();

        }
    }
}
