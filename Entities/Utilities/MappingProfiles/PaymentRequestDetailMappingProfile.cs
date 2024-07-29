using AutoMapper;
using Core.Entities.Concrete.Entities;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailDeleteDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailGetListByPaymentRequestIdDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailGetListDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestDetailUpdateDtos;
using Entities.Concrete.Dtos.PaymentRequestDetailDtos.PaymentRequestGetByIdDtos;

namespace Entities.Utilities.MappingProfiles
{
    public class PaymentRequestDetailMappingProfile: Profile
    {
        public PaymentRequestDetailMappingProfile()
        {
            CreateMap<PaymentRequestDetail, PaymentRequestDetailAddRequestDto>();
            CreateMap<PaymentRequestDetailAddRequestDto, PaymentRequestDetail>();

            CreateMap<PaymentRequestDetail, PaymentRequestDetailGetByIdResponseDto>();
            CreateMap<PaymentRequestDetailGetByIdResponseDto, PaymentRequestDetail>();

            CreateMap<PaymentRequestDetail, PaymentRequestDetailGetListResponseDto>();
            CreateMap<PaymentRequestDetailGetListResponseDto, PaymentRequestDetail>();

            CreateMap<PaymentRequestDetail, PaymentRequestDetailUpdateRequestDto>();
            CreateMap<PaymentRequestDetailUpdateRequestDto, PaymentRequestDetail>();

            CreateMap<PaymentRequestDetail, PaymentRequestDetailDeleteRequestDto>();
            CreateMap<PaymentRequestDetailDeleteRequestDto, PaymentRequestDetail>();

            CreateMap<PaymentRequestDetail, PaymentRequestDetailGetListByPaymentRequestIdResponseDto>();
            CreateMap<PaymentRequestDetailGetListByPaymentRequestIdResponseDto, PaymentRequestDetail>();
        }
    }
}
