﻿using api.Data;
using api.Dtos.Stock;
using api.Helpers;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks =  _context.Stocks.Include(c=>c.Comments).ThenInclude(a => a.AppUser).AsQueryable();
            if(!String.IsNullOrWhiteSpace(query.CompanyName))
            { 
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!String.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!String.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) 
                        : stocks.OrderBy(s=>s.Symbol);
                }


                if (query.SortBy.Equals("Company Name", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.CompanyName)
                        : stocks.OrderBy(s => s.CompanyName);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(s=> s.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s=> s.Symbol == symbol);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(s=>s.Id == id);
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
