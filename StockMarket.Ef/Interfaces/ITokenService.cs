﻿using StockMarketEntites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Ef.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}
