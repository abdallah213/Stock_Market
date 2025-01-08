using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using StockMarketEntites.Helper;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;
        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks
                .FirstOrDefaultAsync(stock => stock.Id == id);

            if(stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks
                .Include(c=>c.Comments)
                .ThenInclude(u => u.AppUser)
                .ToListAsync();
        }

        public async Task<List<Stock>> GetAllAsyncByQuery(QueryObject query)
        {
            var stocks = _context.Stocks
                .Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(x => x.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(x => x.Symbol.Contains(query.Symbol));
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks
                .Include(c=>c.Comments)
                .ThenInclude(u=>u.AppUser)
                .FirstOrDefaultAsync(i=>i.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(x=>x.Symbol.ToLower() == symbol.ToLower());
        }

        public async Task<bool> stockExisy(int id)
        {
            return await _context.Stocks.AnyAsync(i => i.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStcokRequestDto updateStcokDto)
        {
            var esistingStock = await _context.Stocks
                .FirstOrDefaultAsync(x => x.Id == id);

            if(esistingStock == null)
            {
                return null;
            }

            esistingStock.Symbol = updateStcokDto.Symbol;
            esistingStock.LastDivdend = updateStcokDto.LastDivdend;
            esistingStock.Purchase = updateStcokDto.Purchase;
            esistingStock.Industry = updateStcokDto.Industry;
            esistingStock.CompanyName = updateStcokDto.CompanyName;
            esistingStock.MarketCap = updateStcokDto.MarketCap;

            await _context.SaveChangesAsync();
            return esistingStock;
        }
    }
}
