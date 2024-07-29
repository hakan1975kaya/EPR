using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dtos.UserDtos.UserAddDtos;
using Entities.Concrete.Dtos.UserDtos.UserGetByIdDtos;
using Entities.Concrete.Dtos.UserDtos.UserGetListDtos;
using Entities.Concrete.Dtos.UserDtos.UserPasswordChangeDtos;
using Entities.Concrete.Dtos.UserDtos.UserSaveDtos;
using Entities.Concrete.Dtos.UserDtos.UserSearchDtos;
using Entities.Concrete.Dtos.UserDtos.UserUpdateDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<UserGetByIdResponseDto>> GetById(int id);
        Task<IDataResult<List<UserGetListResponseDto>>> GetList();
        Task<IResult> Add(UserAddRequestDto userAddRequestDto);
        Task<IResult> Update(UserUpdateRequestDto userUpdateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<UserSearchResponseDto>>> Search(UserSearchRequestDto userSearchRequestDto);
        Task<IResult> Save(UserSaveRequestDto userSaveRequestDto);
        Task<IResult> PasswordChange(PasswordChangeRequestDto passwordChangeRequestDto);
    }
}
