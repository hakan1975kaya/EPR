using Core.Utilities.Results.Abstract;
using Core.Utilities.Security;
using Entities.Concrete.Dtos.AuthDtos.AccessTokenDtoS;
using Entities.Concrete.Dtos.AuthDtos.LoginDtos;
using Entities.Concrete.Dtos.AuthDtos.RegisterDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.OperationClaimDtos.OperationClaimGetListDtos;
using Entities.Concrete.Dtos.UserDtos.UserGetByRegistrationNumberDtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<AccessToken>> Register(RegisterRequestDto registerRequestDto);
        Task<IDataResult<AccessToken>> Login(LoginRequestDto loginRequestDto);
        Task<IResult> UserExists(int registrationNumber);
        Task<IDataResult<AccessToken>> CreateAccessToken(AccessTokenAddRequestDto accessTokenAddRequestDto);
        Task<IDataResult<UserGetByRegistrationNumberResponseDto>> UserGetByRegistrationNumber(int registrationNumber);
        Task<IDataResult<List<OperationClaimGetListByUserIdResponseDto>>> OperationClaimGetListByUserId(int userId);
        Task<IDataResult<List<MenuListGetByUserIdResponseDto>>> MenuListGetByUserId(int userId);

    }
}
