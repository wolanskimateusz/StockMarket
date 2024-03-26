using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDBContext _context;
        public StockRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null) return null;
            
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null) return null;

            stockModel.Symbol = stockDto.Symbol;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.Industry = stockDto.Industry;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}
