using api.Dtos.Stock;
using api.Models;
using StockMarketEntites.Helper;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<List<Stock>> GetAllAsyncByQuery(QueryObject query);
        Task<Stock?> GetByIdAsync(int id); // Nullably becouse FirstOrDefult Can Be a null
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStcokRequestDto updateStcokDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> stockExisy(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
    } 
}
