using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMarket.Ef.Extension;
using StockMarket.Ef.Interfaces;
using StockMarketEntites.Mappers;
using StockMarketEntites.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController(UserManager<AppUser> userManager,
        IStockRepository stockRepo , IPortfolioRepository portfolioRepo) : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IStockRepository _stockRepo = stockRepo;
        private readonly IPortfolioRepository _portfolioRepo = portfolioRepo;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var userName = User.GetUsername();
            var appUser  = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepo.GetUserPotrtfolio(appUser);

            return Ok(new { user = appUser.AppUserToUserDto(), userPortfolio });

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            { return BadRequest("Stock Not Found"); }

            var userPortfolio = await _portfolioRepo.GetUserPotrtfolio(appUser);
            if(userPortfolio.Any(e=>e.Symbol.ToLower() == symbol.ToLower()))
            { return BadRequest("Can not Add Same Stock In Portfolio"); }

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };
            if (portfolioModel == null)
            { return StatusCode(500, "not create"); }

            await _portfolioRepo.AddPortfolioAsync(portfolioModel);

            return Created(); 
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepo.GetUserPotrtfolio(appUser);

            var filterStocks = userPortfolio
                .Where(x=>x.Symbol.ToLower() == symbol.ToLower());

            if (filterStocks.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolioAsync(appUser,symbol);
            }
            else
            {
                return BadRequest("Stock not In Your Portfolio");
            }

            return Ok();
        }

    }
}
