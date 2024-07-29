using Core.DataAccess.Abstract;
using Entities.Concrete.Dtos.MenuDtos.MenuChildListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuListGetByUserIdResponseDtos;
using Entities.Concrete.Dtos.MenuDtos.MenuParentListGetByUserIdResponseDtos;
using Entities.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IMenuDal : IEntityRepository<Menu>
    {
        Task<List<MenuListGetByUserIdResponseDto>> MenuListGetByUserId(int userId);
        Task<List<MenuParentListGetByUserIdResponseDto>> MenuParentListGetByUserId(int userId);
        Task<List<MenuChildListGetByUserIdResponseDto>> MenuChildListGetByUserId(int userId);
    }
}
