using AutoMapper;
using System.Text;
using System.Text.Json;
using Tandem.Abstract;
using Tandem.Dtos.TandemDtos.TandemCorporateDefineExternalDtos;
using Tandem.Dtos.TandemDtos.TandemCorporateDefineInternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentInquiryExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentInquiryInternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentTransferInternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentUpdateExternalDtos;
using Tandem.Dtos.TandemDtos.TandemPaymentUpdateInternalDtos;
using Tandem.Utilities.Constants;

namespace Tandem.Concrete
{
    public class TandemDal : ITandemDal
    {
        IMapper _mapper;
        public TandemDal(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<TandemCorporateDefineResponseExternalDto> CorporateDefine(TandemCorporateDefineRequestExternalDto tandemCorporateDefineRequestExternalDto)
        {
            var client = new HttpClient();

            var corporateExternal = tandemCorporateDefineRequestExternalDto.CorporateExternal;

            var corporateInternal = new CorporateInternal
            {
                CorporateAccountNo = corporateExternal.CorporateAccountNo,
                CorporateCode = corporateExternal.CorporateCode,
                CorporateName = corporateExternal.CorporateName,
                MoneyType = corporateExternal.MoneyType,
                Prefix = corporateExternal.Prefix
            };

            var tandemCorporateDefineRequestInternalDto = new TandemCorporateDefineRequestInternalDto
            {
                CorporateInternal = corporateInternal,
            };

            var body = JsonSerializer.Serialize<CorporateInternal>(tandemCorporateDefineRequestInternalDto.CorporateInternal);

            var data = new StringContent(body, Encoding.UTF8, "application/json");

            var url = TandemConstants.BaseUrl + "kurumTanimla";

            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    var tandemPaymentUpdateResponseInternalDto = JsonSerializer.Deserialize<TandemPaymentUpdateResponseInternalDto>(content);

                    if (tandemPaymentUpdateResponseInternalDto != null)
                    {
                        return new TandemCorporateDefineResponseExternalDto
                        {
                            ResponseCode = tandemPaymentUpdateResponseInternalDto.ResponseCode,
                            ResponseMessage = tandemPaymentUpdateResponseInternalDto.ResponseMessage
                        };
                    }
                }
            }
            return null;
        }

        public async Task<TandemPaymentInquiryResponseExternalDto> PaymentInquiry(TandemPaymentInquiryRequestExternalDto tandemPaymentInquiryRequestExternalDto)
        {

            var client = new HttpClient();

            var tandemPaymentInquiryRequestInternalDto = new TandemPaymentInquiryRequestInternalDto
            {
                CorporateCode = tandemPaymentInquiryRequestExternalDto.CorporateCode,
                Status = tandemPaymentInquiryRequestExternalDto.Status,
            };

            var body = JsonSerializer.Serialize<TandemPaymentInquiryRequestInternalDto>(tandemPaymentInquiryRequestInternalDto);

            var data = new StringContent(body, Encoding.UTF8, "application/json");

            var url = TandemConstants.BaseUrl + "odemeSorgu";

            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    var tandemPaymentInquiryResponseInternalDto = JsonSerializer.Deserialize<TandemPaymentInquiryResponseInternalDto>(content);

                    if (tandemPaymentInquiryResponseInternalDto != null)
                    {
                        var summaryExternals = new List<SummaryExternal>();

                        foreach (var summaryInternal in tandemPaymentInquiryResponseInternalDto.SummaryInternals)
                        {
                            var summaryExternal = new SummaryExternal
                            {
                                Amount = summaryInternal.Amount,
                                CorporateCode = summaryInternal.CorporateCode,
                                Quantity = summaryInternal.Quantity,
                                RegistrationNumber = summaryInternal.RegistrationNumber,
                                RequestNumber = summaryInternal.RequestNumber,
                                Status = summaryInternal.Status,
                                SystemEnteredDate = Convert.ToDateTime(summaryInternal.SystemEnteredDate.ToString().Substring(0, 4) + "-" + summaryInternal.SystemEnteredDate.ToString().Substring(4, 2) + "-" + summaryInternal.SystemEnteredDate.ToString().Substring(6, 2)),
                                SystemEnteredRegistrationNumber = summaryInternal.SystemEnteredRegistrationNumber,
                                SystemEnteredTime = TimeSpan.FromMilliseconds(summaryInternal.SystemEnteredTime),
                                UploadDate = Convert.ToDateTime(summaryInternal.UploadDate.ToString().Substring(0, 4) + "-" + summaryInternal.UploadDate.ToString().Substring(4, 2) + "-" + summaryInternal.UploadDate.ToString().Substring(6, 2)),
                            };
                            summaryExternals.Add(summaryExternal);
                        }


                        return new TandemPaymentInquiryResponseExternalDto
                        {
                            ResponseCode = tandemPaymentInquiryResponseInternalDto.ResponseCode,
                            ResponseMessage = tandemPaymentInquiryResponseInternalDto.ResponseMessage,
                            SummaryExternals = summaryExternals
                        };
                    }
                }
            }
            return null;
        }
        public async Task<TandemPaymentTransferResponseExternalDto> PaymentTransfer(TandemPaymentTransferRequestExternalDto tandemPaymentTransferRequestExternalDto)
        {
            var client = new HttpClient();

            var paymentInternals = new List<PaymentInternal>();

            foreach (var paymentExternal in tandemPaymentTransferRequestExternalDto.PaymentExternals)
            {
                var paymentInternal = new PaymentInternal
                {
                    AccountNumber = paymentExternal.AccountNumber,
                    CardDepositDate = String.Format("{0:yyyyMMdd}", paymentExternal.CardDepositDate),
                    CustomerNumber = paymentExternal.CustomerNumber,
                    Explanation = paymentExternal.Explanation,
                    FirstName = paymentExternal.FirstName,
                    LastName = paymentExternal.LastName,
                    PaymentAmount = paymentExternal.PaymentAmount,
                    PhoneNumber = paymentExternal.PhoneNumber,
                    ReferenceNumber = paymentExternal.ReferenceNumber,

                };
                paymentInternals.Add(paymentInternal);
            }

            var tandemPaymentTransferRequestInternalDto = new TandemPaymentTransferRequestInternalDto
            {
                CorporateCode = tandemPaymentTransferRequestExternalDto.CorporateCode,
                MoneyType = tandemPaymentTransferRequestExternalDto.MoneyType.ToString(),
                PaymentInternals = paymentInternals,
                RegistrationNumber = tandemPaymentTransferRequestExternalDto.RegistrationNumber,
                RequestNumber = tandemPaymentTransferRequestExternalDto.RequestNumber
            };

            var body = JsonSerializer.Serialize<TandemPaymentTransferRequestInternalDto>(tandemPaymentTransferRequestInternalDto);

            var data = new StringContent(body, Encoding.UTF8, "application/json");

            var url = TandemConstants.BaseUrl + "odemeAktarma";

            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    var tandemPaymentTransferResponseInternalDto = JsonSerializer.Deserialize<TandemPaymentTransferResponseInternalDto>(content);

                    if (tandemPaymentTransferResponseInternalDto != null)
                    {
                        return new TandemPaymentTransferResponseExternalDto
                        {
                            ResponseCode = tandemPaymentTransferResponseInternalDto.ResponseCode,
                            ResponseMessage = tandemPaymentTransferResponseInternalDto.ResponseMessage
                        };
                    }
                }
            }
            return null;
        }

        public async Task<TandemPaymentUpdateResponseExternalDto> PaymentUpdate(TandemPaymentUpdateRequestExternalDto tandemPaymentUpdateRequestExternalDto)
        {
            var client = new HttpClient();

            var requestListInternals = new List<RequestListInternal>();

            foreach (var requestListExternal in tandemPaymentUpdateRequestExternalDto.RequestListExternals)
            {
                var requestListInternal = new RequestListInternal
                {
                    CorporateCode = requestListExternal.CorporateCode,
                    RegistrationNumber = requestListExternal.RegistrationNumber,
                    RequestNumber = requestListExternal.RequestNumber,
                    Status = requestListExternal.Status,
                };
                requestListInternals.Add(requestListInternal);
            }

            var tandemPaymentUpdateRequestInternalDto = new TandemPaymentUpdateRequestInternalDto
            {
                RequestListInternals = requestListInternals,
            };

            var body = JsonSerializer.Serialize<TandemPaymentUpdateRequestInternalDto>(tandemPaymentUpdateRequestInternalDto);

            var data = new StringContent(body, Encoding.UTF8, "application/json");

            var url = TandemConstants.BaseUrl + "odemeGuncelle";

            var response = await client.PostAsync(url, data);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(content))
                {
                    var tandemPaymentUpdateResponseInternalDto = JsonSerializer.Deserialize<TandemPaymentUpdateResponseInternalDto>(content);

                    if (tandemPaymentUpdateResponseInternalDto != null)
                    {
                        return new TandemPaymentUpdateResponseExternalDto
                        {
                            ResponseCode = tandemPaymentUpdateResponseInternalDto.ResponseCode,
                            ResponseMessage = tandemPaymentUpdateResponseInternalDto.ResponseMessage
                        };
                    }
                }
            }
            return null;
        }

    }
}
