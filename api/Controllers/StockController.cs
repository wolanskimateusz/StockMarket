﻿using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly AppDBContext _context;
        public StockController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var stocks = _context.Stocks.ToList();

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var stock = _context.Stocks.Find(id);

            if(stock == null) 
            {
                return NotFound();
            }

            return Ok(stock);
        }
    }
}
