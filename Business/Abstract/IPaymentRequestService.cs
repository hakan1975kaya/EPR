using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSaveDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestSearchDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestAddDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByIdDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetListDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestUpdateDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByTodayDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestDownloadDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetByRequestNumberDtos;
using Entities.Concrete.Dtos.PaymentRequestDtos.PaymentRequestGetListByCorporateIdDtos;

namespace Business.Abstract
{
    public interface IPaymentRequestService
    {
        Task<IDataResult<PaymentRequestGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<PaymentRequestGetByRequestNumberResponseDto>> GetByRequestNumber(string requestNumber);
        Task<IDataResult<List<PaymentRequestGetListResponseDto>>> GetList();
        Task<IResult> Add(PaymentRequestAddRequestDto paymentRequestAddRequestDto);
        Task<IResult> Update(PaymentRequestUpdateRequestDto paymentRequestUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<PaymentRequestSearchResponseDto>>> Search(PaymentRequestSearchRequestDto paymentRequestSearchRequestDto);
        Task<IResult> Save(PaymentRequestSaveRequestDto paymentRequestSaveRequestDto);
        Task<IDataResult<List<PaymentRequestGetByTodayResponseDto>>> GetByToday();
        Task<IDataResult<PaymentRequestDownloadResponseDto>> PaymentRequestDownload(PaymentRequestDownloadRequestDto paymentRequestDownloadRequestDto);
        Task<IDataResult<List<PaymentRequestGetListByCorporateIdResponseDto>>> GetListByCorporateId(int corporateId);
    }

}

