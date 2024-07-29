using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.CorporateDtos.CorporateAddDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetByIdDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetListDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateUpdateDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSaveDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateSearchDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetListPrefixAvailableDtos;
using Entities.Concrete.Dtos.CorporateDtos.CorporateGetByCorporateCodeDtos;

namespace Business.Abstract
{
    public interface ICorporateService
    {
        Task<IDataResult<CorporateGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<CorporateGetListResponseDto>>> GetList();
        Task<IResult> Add(CorporateAddRequestDto corporateAddRequestDto);
        Task<IResult> Update(CorporateUpdateRequestDto corporateUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<CorporateSearchResponseDto>>> Search(CorporateSearchRequestDto corporateSearchRequestDto);
        Task<IResult> Save(CorporateSaveRequestDto corporateSaveRequestDto);
        Task<IDataResult<List<CorporateGetListPrefixAvailableResponseDto>>> GetListPrefixAvailable();
        Task<IDataResult<CorporateGetByCorporateCodeResponseDto>> GetByCorporateCode(int corporateCode);
    }
}
