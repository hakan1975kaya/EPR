using AutoMapper;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation.UserValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Entities.Concrete.Entities;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete.Dtos.UserDtos.UserGetByIdDtos;
using Entities.Concrete.Dtos.UserDtos.UserGetListDtos;
using Entities.Concrete.Dtos.UserDtos.UserAddDtos;
using Entities.Concrete.Dtos.UserDtos.UserUpdateDtos;
using Entities.Concrete.Dtos.UserDtos.UserSearchDtos;
using Entities.Concrete.Dtos.UserDtos.UserSaveDtos;
using Entities.Concrete.Enums.GeneralEnums;
using Business.ValidationRules.FluentValidation.MenuValidators;
using Core.Aspects.Autofac.Transaction;
using Entities.Concrete.Dtos.UserDtos.UserPasswordChangeDtos;
using Entities.Concrete.Dtos.AuthDtos.RegisterDtos;

namespace Business.Concrete
{
    [LogAspect(typeof(DatabaseLogger), Priority = 1)]
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IMapper _mapper;
        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        [SecurityAspect("User.Add", Priority = 2)]
        [ValidationAspect(typeof(UserAddRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IUserService.Get", Priority = 4)]
        public async Task<IResult> Add(UserAddRequestDto userAddRequestDto)
        {
            var user = await _userDal.Get(x => x.RegistrationNumber == userAddRequestDto.RegistrationNumber && x.IsActive == true);

            if (user == null)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userAddRequestDto.Password, out passwordHash, out passwordSalt);

                user = _mapper.Map<User>(userAddRequestDto);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.IsActive = true;

                await _userDal.Add(user);

                return new SuccessResult(UserMessages.Added);
            }

            return new ErrorResult(UserMessages.UserAlreadyExists);
        }

        [SecurityAspect("User.Update", Priority = 2)]
        [ValidationAspect(typeof(UserUpdateRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IUserService.Get", Priority = 4)]
        public async Task<IResult> Update(UserUpdateRequestDto userUpdateRequestDto)
        {
            var user = _mapper.Map<User>(userUpdateRequestDto);
            if (user != null)
            {
                if (string.IsNullOrEmpty(userUpdateRequestDto.Password))
                {
                    user = _mapper.Map<User>(userUpdateRequestDto);
                    user.PasswordHash = user.PasswordHash;
                    user.PasswordSalt = user.PasswordSalt;
                    user.IsActive = true;
                }
                else
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(userUpdateRequestDto.Password, out passwordHash, out passwordSalt);

                    user = _mapper.Map<User>(userUpdateRequestDto);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.IsActive = true;
                }
                await _userDal.Update(user);

                return new SuccessResult(UserMessages.Updated);
            }
            return new ErrorResult(UserMessages.OperationFailed);
        }

        [SecurityAspect("User.Delete", Priority = 2)]
        [CacheRemoveAspect("IUserService.Get", Priority = 3)]
        public async Task<IResult> Delete(int id)
        {
            var user = await _userDal.Get(x => x.Id == id && x.IsActive == true);
            if (user != null)
            {
                user.IsActive = false;
                await _userDal.Update(user);
                return new SuccessResult(UserMessages.Deleted);

            }
            return new ErrorResult(UserMessages.OperationFailed);
        }

        [SecurityAspect("User.GetById", Priority = 2)]
        public async Task<IDataResult<UserGetByIdResponseDto>> GetById(int id)
        {
            var user = await _userDal.Get(x => x.Id == id && x.IsActive == true);
            var userGetByIdResponseDto = _mapper.Map<UserGetByIdResponseDto>(user);
            return new SuccessDataResult<UserGetByIdResponseDto>(userGetByIdResponseDto);
        }

        [SecurityAspect("User.GetList", Priority = 2)]
        public async Task<IDataResult<List<UserGetListResponseDto>>> GetList()
        {
            var users = await _userDal.GetList(x => x.IsActive == true);
            var userGetListResponseDtos = _mapper.Map<List<UserGetListResponseDto>>(users);
            return new SuccessDataResult<List<UserGetListResponseDto>>(userGetListResponseDtos);
        }

        [SecurityAspect("User.Search", Priority = 2)]
        [ValidationAspect(typeof(UserSearchRequestDtoValidator), Priority = 3)]
        public async Task<IDataResult<List<UserSearchResponseDto>>> Search(UserSearchRequestDto userSearchRequestDto)
        {
            var filter = userSearchRequestDto.Filter;
            var users = await _userDal.GetList(x => (x.RegistrationNumber.ToString().ToLower().Contains(filter.ToLower()) || x.FirstName.ToLower().Contains(filter.ToLower()) || x.LastName.ToLower().Contains(filter.ToLower())) && x.IsActive == true);
            var userSearchResponseDtos = _mapper.Map<List<UserSearchResponseDto>>(users.OrderBy(x => x.RegistrationNumber).ToList());
            return new SuccessDataResult<List<UserSearchResponseDto>>(userSearchResponseDtos);
        }

        [SecurityAspect("User.Save", Priority = 2)]
        [ValidationAspect(typeof(UserSaveRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IUserService.Get", Priority = 4)]
        [TransactionAspect(Priority = 5)]
        public async Task<IResult> Save(UserSaveRequestDto userSaveRequestDto)
        {
            var saveType = userSaveRequestDto.SaveType;
            var isPasswordSeted = userSaveRequestDto.IsPasswordSeted;
            var userDto = userSaveRequestDto.User;
            byte[] passwordHash, passwordSalt;
            if (isPasswordSeted)
            {
                HashingHelper.CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);
            }
            else
            {
                var currentUser = await _userDal.Get(x => x.Id == userDto.Id && x.IsActive == true);
                if (currentUser != null)
                {
                    passwordHash = currentUser.PasswordHash;
                    passwordSalt = currentUser.PasswordSalt;
                }
                else
                {
                    passwordHash = null;
                    passwordSalt = null;
                }
            }

            var user = _mapper.Map<User>(userDto);

            if (saveType == SaveTypeEnum.Add)
            {
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;   

                user.IsActive = true;

                await _userDal.Add(user);

                return new SuccessResult(UserMessages.Added);
            }
            else if (saveType == SaveTypeEnum.Update)
            {
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                user.IsActive = true;

                await _userDal.Update(user);

                return new SuccessResult(UserMessages.Updated);
            }

            return new ErrorResult(UserMessages.OperationFailed);

        }

        [SecurityAspect("User.PasswordChange", Priority = 2)]
        [ValidationAspect(typeof(PasswordChangeRequestDtoValidator), Priority = 3)]
        [CacheRemoveAspect("IUserService.Get", Priority = 4)]
        public async Task<IResult> PasswordChange(PasswordChangeRequestDto passwordChangeRequestDto)
        {
            var registrationNumber = passwordChangeRequestDto.RegistrationNumber;
            var password = passwordChangeRequestDto.Password;

            var user = await _userDal.Get(x => x.RegistrationNumber == registrationNumber && x.IsActive == true);
            if(user != null)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userDal.Update(user);

                return new SuccessResult(UserMessages.PasswordChanged);
            }

            return new ErrorResult(UserMessages.OperationFailed);
        }
    }
}
