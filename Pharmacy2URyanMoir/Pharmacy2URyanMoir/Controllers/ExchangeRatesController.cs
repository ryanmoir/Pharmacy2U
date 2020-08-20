using System.Collections.Generic;
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
    public class ExchangeRatesController : ControllerBase
    {
        private readonly DbSetContext _context;

        public ExchangeRatesController()
        {
            _context = new DbSetContext();
        }

        // GET: api/ExchangeRates/AllExchangeRates
        [HttpGet]
        [ActionName("AllExchangeRates")]
        public async Task<ActionResult<IEnumerable<ExchangeRates>>> GetExchangeRates()
        {
            return await _context.ExchangeRates.ToListAsync();
        }

        // GET: api/ExchangeRates/ExchangeRate/5
        [HttpGet("{id}")]
        [ActionName("ExchangeRate")]
        public async Task<ActionResult<ExchangeRates>> GetExchangeRate(int id)
        {
            var exchangeRates = await _context.ExchangeRates.FindAsync(id);

            if (exchangeRates == null)
            {
                return NotFound();
            }

            return exchangeRates;
        }

        // GET: api/ExchangeRates/ExchangeRateIds/baseCurrency=a&convertedCurrency=b
        [HttpGet("{baseCurrency}/{convertedCurrency}")]
        [ActionName("ExchangeRateIds")]
        public async Task<ActionResult<ExchangeRates>> GetExchangeRate(int baseCurrency, int convertedCurrency)
        {
            var query = await _context.ExchangeRates
                .Where(s => s.BaseCurrecncy == baseCurrency && s.ConvertedCurrency == convertedCurrency)         
                .FirstOrDefaultAsync();

            if (query == null)
            {
                return NotFound();
            }

            return query;
        }

        // GET: api/ExchangeRates/ExchangeRateStrings/baseCurrency=a/convertedCurrency=b
        [HttpGet("{baseCurrency}/{convertedCurrency}")]
        [ActionName("ExchangeRateStrings")]
        public async Task<ActionResult<ExchangeRates>> GetExchangeRate(string baseCurrency, string convertedCurrency)
        {
            var currenciesController = new CurrenciesController();
            var baseCurrencyResult = await currenciesController.GetCurrencies(baseCurrency.Replace("baseCurrency=",""));
            var convertedCurrencyResult = await currenciesController.GetCurrencies(convertedCurrency.Replace("convertedCurrency=", ""));
            if (baseCurrencyResult.Value == null || convertedCurrencyResult.Value == null)
            {
                return NotFound();
            }

            var query = await _context.ExchangeRates
                .Where(s => s.BaseCurrecncy == baseCurrencyResult.Value.Id && s.ConvertedCurrency == convertedCurrencyResult.Value.Id)
                .FirstOrDefaultAsync();

            if (query == null)
            {
                return NotFound();
            }
            return query;
        }

        // POST: api/ExchangeRates/AddExchangeRate
        [HttpPost]
        [ActionName("AddExchangeRate")]
        public async Task<ActionResult<ExchangeRates>> AddExchangeRate(ExchangeRates exchangeRates)
        {
            _context.ExchangeRates.Add(exchangeRates);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExchangeRates", new { id = exchangeRates.Id }, exchangeRates);
        }

        [HttpPost]
        [ActionName("AddExchangeRate")]
        public async Task<ActionResult<ExchangeRates>> AddExchangeRate(string baseCurrency, string convertedCurrency, float exchangeRate)
        {
            var currencyController = new CurrenciesController();
            var baseCurrencyId = await currencyController.GetCurrencies(baseCurrency);
            var convertedCurrencyId = await currencyController.GetCurrencies(convertedCurrency);
            if (baseCurrencyId.Value == null || convertedCurrencyId.Value == null)
            {
                return NotFound();
            }

            var exchangeRateObj = new ExchangeRates
            {
                BaseCurrecncy = baseCurrencyId.Value.Id,
                ConvertedCurrency = convertedCurrencyId.Value.Id,
                ExchangeRate = exchangeRate
            };
            _context.ExchangeRates.Add(exchangeRateObj);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExchangeRates", exchangeRateObj);
        }

        // PUT: api/ExchangeRates/UpdateExchangeRate/5
        [HttpPut("{id}")]
        [ActionName("UpdateExchangeRate")]
        public async Task<IActionResult> UpdateExchangeRate(int id, ExchangeRates exchangeRates)
        {
            if (id != exchangeRates.Id)
            {
                return BadRequest();
            }

            _context.Entry(exchangeRates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExchangeRatesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ExchangeRates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExchangeRates>> DeleteExchangeRates(int id)
        {
            var exchangeRates = await _context.ExchangeRates.FindAsync(id);
            if (exchangeRates == null)
            {
                return NotFound();
            }

            _context.ExchangeRates.Remove(exchangeRates);
            await _context.SaveChangesAsync();

            return exchangeRates;
        }

        private bool ExchangeRatesExists(int id)
        {
            return _context.ExchangeRates.Any(e => e.Id == id);
        }
    }
}
