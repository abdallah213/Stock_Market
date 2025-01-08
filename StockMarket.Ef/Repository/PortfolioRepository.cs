using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using StockMarket.Ef.Interfaces;
using StockMarketEntites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Ef.Repository
{
    public class PortfolioRepository(AppDbContext context) : IPortfolioRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Portfolio?> AddPortfolioAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolioAsync(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolios
                .FirstOrDefaultAsync(x => x.AppUserId == appUser.Id
                && x.Stock.Symbol == symbol);

            if (portfolioModel == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPotrtfolio(AppUser user)
        {
            var stocks = _context.Portfolios.Where(u=>u.AppUserId == user.Id)
                .Select(stock=> new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDivdend = stock.Stock.LastDivdend,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap,
                });

            return await stocks.ToListAsync();
        }
    }
}
