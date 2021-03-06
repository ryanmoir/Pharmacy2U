﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy2URyanMoir.Data;
using Pharmacy2URyanMoir.Models;

namespace Pharmacy2URyanMoir.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly DbSetContext _context;

        public CurrenciesController()
        {
            _context = new DbSetContext();
        }

        // GET: api/Currencies/AllCurrencies
        [HttpGet]
        [ActionName("AllCurrencies")]
        public async Task<ActionResult<IEnumerable<Currencies>>> GetCurrencies()
        {
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/Currencies/Currency/5
        [HttpGet("{arg}")]
        [ActionName("Currency")]
        public async Task<ActionResult<Currencies>> GetCurrencies(string arg)
        {
            if (arg == null)
            {
                return NotFound();
            }

            arg = arg.Replace("arg=", "");
            if (int.TryParse(arg, out var currencyId))
            {
                var currency = await _context.Currencies.FindAsync(currencyId);
                if (currency != null)
                {
                    return CreatedAtAction("GetCurrencies", new { id = currency.Id }, currency);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                var query = await _context.Currencies
                    .Where(s => s.Name == arg)
                    .FirstOrDefaultAsync();

                if (query == null)
                {
                    return NotFound();
                }

                return query;
            }
        }

        // GET: api/Currencies/CurrencyId/5
        /*[HttpGet("{id}")]
        [ActionName("CurrencyId")]
        public async Task<ActionResult<Currencies>> GetCurrencies(string currencyIdStr)
        {
            var currencyId = int.Parse(currencyIdStr);
            var currency = await _context.Currencies.FindAsync(currencyId);
            if (currency != null)
            {
                return CreatedAtAction("GetCurrencies", new { id = currency.Id }, currency);
            }
            else
            {
                return NotFound();
            }
        }*/


        // POST: api/Currencies/AddCurrency
        [HttpPost]
        [ActionName("AddCurrency")]
        public async Task<ActionResult<Currencies>> AddCurrency(Currencies currencies)
        {
            _context.Currencies.Add(currencies);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCurrencies", new { id = currencies.Id }, currencies);
        }

        // POST: api/Currencies/UpdateCurrency
        [HttpPost]
        [ActionName("UpdateCurrency")]
        public async Task<ActionResult<Currencies>> UpdateCurrency(Currencies currencies)
        {
            var currency = await _context.Currencies.FindAsync(currencies.Id);
            if (currency != null)
            {
                currency = currencies;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCurrencies", new { id = currency.Id }, currency);

            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Currencies/DeleteCurrency/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCurrency")]
        public async Task<ActionResult<Currencies>> DeleteCurrencies(int id)
        {
            var currencies = await _context.Currencies.FindAsync(id);
            if (currencies == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(currencies);
            await _context.SaveChangesAsync();

            return currencies;
        }
    }
}
