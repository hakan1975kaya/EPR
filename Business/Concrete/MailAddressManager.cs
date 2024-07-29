using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.CorporateMailAddressValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance.Dtos;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressAddDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressGetByIdDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressGetListDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSaveDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressSearchDtos;
using Entities.Concrete.Dtos.MailAddressDtos.MailAddressUpdateDtos;
using Entities.Concrete.Enums.GeneralEnums;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class MailAddressManager : IMailAddressService
    {
        private IMailAddressDal _mailAddressDal;
        private IMapper _mapper;
        public MailAddressManager(IMailAddressDal mailAddressDal, IMapper mapper)
        {
            _mailAddressDal = mailAddressDal;
            _mapper = mapper;
        }

        [SecurityAspect("MailAddress.Add", Priority = 2)]
        [ValidationAspect(typeof(MailAddressAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMailAddressService.Get", Priority = 4)]
        public async Task<IResult> Add(MailAddressAddRequestDto mailAddressAddRequestDto)
        {
            var mailAddress = _mapper.Map<MailAddress>(mailAddressAddRequestDto);
            await _mailAddressDal.Add(mailAddress);
            return new SuccessResult(MailAddressMessages.Added);
        }

        [SecurityAspect("MailAddress.Delete", Priority = 2)]
        [CacheRemoveAspect("IMailAddressService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var mailAddress = await _mailAddressDal.Get(x => x.Id == id && x.IsActive == true);
            if (mailAddress != null)
            {
                mailAddress.IsActive = false;
                await _mailAddressDal.Update(mailAddress);
            }
            return new SuccessResult(MailAddressMessages.Deleted);
        }

        [SecurityAspect("MailAddress.GetById", Priority = 2)]
        public async Task<IDataResult<MailAddressGetByIdResponseDto>> GetById(int id)
        {
            var mailAddress = await _mailAddressDal.Get(x => x.Id == id && x.IsActive == true);
            var mailAddressGetByIdResponseDto = _mapper.Map<MailAddressGetByIdResponseDto>(mailAddress);
            return new SuccessDataResult<MailAddressGetByIdResponseDto>(mailAddressGetByIdResponseDto);
        }

        [SecurityAspect("MailAddress.GetList", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<MailAddressGetListResponseDto>>> GetList()
        {
            var mailAddresss = await _mailAddressDal.GetList(x => x.IsActive == true);
            var mailAddressGetListResponseDtos = _mapper.Map<List<MailAddressGetListResponseDto>>(mailAddresss);
            mailAddressGetListResponseDtos = mailAddressGetListResponseDtos.OrderBy(x => x.Optime).OrderBy(x => x.Address).ToList();
            return new SuccessDataResult<List<MailAddressGetListResponseDto>>(mailAddressGetListResponseDtos);
        }

        [SecurityAspect("MailAddress.GetListNotPtt", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<MailAddressGetListNotPttResposeDto>>> GetListNotPtt()
        {
            var mailAddresss = await _mailAddressDal.GetList(x => x.IsPtt != true && x.IsActive == true);
            var mailAddressGetListNotPttResposeDtos = _mapper.Map<List<MailAddressGetListNotPttResposeDto>>(mailAddresss);
            mailAddressGetListNotPttResposeDtos = mailAddressGetListNotPttResposeDtos.OrderBy(x => x.Optime).OrderBy(x => x.Address).ToList();
            return new SuccessDataResult<List<MailAddressGetListNotPttResposeDto>>(mailAddressGetListNotPttResposeDtos);
        }

        [SecurityAspect("MailAddress.GetListPtt", Priority = 2)]
        [CacheAspect(Priority = 3)]
        public async Task<IDataResult<List<MailAddressGetListPttResposeDto>>> GetListPtt()
        {
            var mailAddresss = await _mailAddressDal.GetList(x => x.IsPtt == true && x.IsActive == true);
            var mailAddressGetListPttResposeDtos = _mapper.Map<List<MailAddressGetListPttResposeDto>>(mailAddresss);
            mailAddressGetListPttResposeDtos = mailAddressGetListPttResposeDtos.OrderBy(x => x.Optime).OrderBy(x => x.Address).ToList();
            return new SuccessDataResult<List<MailAddressGetListPttResposeDto>>(mailAddressGetListPttResposeDtos);
        }

        [SecurityAspect("MailAddress.Save", Priority = 2)]
        [ValidationAspect(typeof(MailAddressSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMailAddressService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(MailAddressSaveRequestDto mailAddressSaveRequestDto)
        {
            var mailAddress = mailAddressSaveRequestDto.MailAddress;
            var saveType = mailAddressSaveRequestDto.SaveType;
            if (saveType == SaveTypeEnum.Add)
            {
                mailAddress.IsActive = true;
                mailAddress.Optime = DateTime.Now;
                await _mailAddressDal.Add(mailAddress);

                return new SuccessResult(MailAddressMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                mailAddress.IsActive = true;
                mailAddress.Optime = DateTime.Now;
                await _mailAddressDal.Update(mailAddress);

                return new SuccessResult(MailAddressMessages.Updated);
            }

            return new ErrorResult(MailAddressMessages.OperationFailed);
        }

        [SecurityAspect("MailAddress.Search", Priority = 2)]
        public async Task<IDataResult<List<MailAddressSearchResponseDto>>> Search(MailAddressSearchRequestDto mailAddressSearchRequestDto)
        {
            return new SuccessDataResult<List<MailAddressSearchResponseDto>>(await _mailAddressDal.Search(mailAddressSearchRequestDto));
        }

        [SecurityAspect("MailAddress.Update", Priority = 2)]
        [ValidationAspect(typeof(MailAddressUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IMailAddressService.Get", Priority = 4)]
        public async Task<IResult> Update(MailAddressUpdateRequestDto mailAddressUpdateRequestDto)
        {
            var mailAddress = _mapper.Map<MailAddress>(mailAddressUpdateRequestDto);
            if (mailAddress != null)
            {
                await _mailAddressDal.Update(mailAddress);
                return new SuccessResult(MailAddressMessages.Updated);

            }
            return new ErrorResult(MailAddressMessages.OperationFailed);

        }

















    }
}
