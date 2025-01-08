using StockMarketEntites.Dtos.Account;
using StockMarketEntites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarketEntites.Mappers
{
    public static class UserMapper
    {
        public  static UserDeatils AppUserToUserDto(this AppUser user)
        {
            return new UserDeatils
            {
                Id = user.Id,
                Email =user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
        }
    }
}
