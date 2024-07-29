using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryAddDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetByIdDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryGetListDtos;
using Entities.Concrete.Dtos.PaymentRequestSummaryDtos.PaymentRequestSummaryUpdateDtos;
using Entities.Concrete.Entities;

namespace Entities.Utilities.MappingProfiles
{
    public class PaymentRequestSummaryMappingProfile : Profile
    {
        public PaymentRequestSummaryMappingProfile()
        {
            CreateMap<PaymentRequestSummary, PaymentRequestSummaryAddRequestDto>();
            CreateMap<PaymentRequestSummaryAddRequestDto, PaymentRequestSummary>();

            CreateMap<PaymentRequestSummary, PaymentRequestSummaryGetByIdResponseDto>();
            CreateMap<PaymentRequestSummaryGetByIdResponseDto, PaymentRequestSummary>();

            CreateMap<PaymentRequestSummary, PaymentRequestSummaryGetListByRequestIdResponseDto>();
            CreateMap<PaymentRequestSummaryGetListByRequestIdResponseDto, PaymentRequestSummary>();

            CreateMap<PaymentRequestSummary, PaymentRequestSummaryUpdateRequestDto>();
            CreateMap<PaymentRequestSummaryUpdateRequestDto, PaymentRequestSummary>();

            CreateMap<PaymentRequest, PaymentRequestSummaryGetByTodayResponseDto>();
            CreateMap<PaymentRequestSummaryGetByTodayResponseDto, PaymentRequest>();

        }
    }
}
