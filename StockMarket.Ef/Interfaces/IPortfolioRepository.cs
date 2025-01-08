using api.Models;
using StockMarketEntites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Ef.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPotrtfolio(AppUser user);
        Task<Portfolio?> AddPortfolioAsync(Portfolio portfolio);
        Task<Portfolio?> DeletePortfolioAsync(AppUser appUser , string symbol);
    }
}
