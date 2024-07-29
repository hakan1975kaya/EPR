using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.RoleDtos.RoleAddDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleGetByIdDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleGetListDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleUpdateDtos;
using Entities.Concrete.Dtos.RoleDtos.RoleSearchDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Dtos.RoleDtos.RoleSaveDtos;

namespace Business.Abstract
{
    public interface IRoleService
    {
        Task<IDataResult<RoleGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<RoleGetListResponseDto>>> GetList();
        Task<IResult> Add(RoleAddRequestDto roleAddRequestDto);
        Task<IResult> Update(RoleUpdateRequestDto roleUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<RoleSearchResponseDto>>> Search(RoleSearchRequestDto roleSearchRequestDto);
        Task<IResult> Save(RoleSaveRequestDto roleSaveRequestDto);
    }
}
