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
    public class CurrenciesController : ControllerBase
    {
        private readonly Pharmacy2UContext _context;

        public CurrenciesController()
        {
            _context = new Pharmacy2UContext();
        }

        // GET: api/Currencies
        [HttpGet]
        [ActionName("AllCurrencies")]
        public async Task<ActionResult<IEnumerable<Currencies>>> GetCurrencies()
        {
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        [ActionName("CurrencyName")]
        public async Task<ActionResult<Currencies>> GetCurrencies(string currencyName)
        {
            var query = await _context.Currencies
                .Where(s => s.Name == currencyName)
                .FirstOrDefaultAsync();

            if (query == null)
            {
                return NotFound();
            }

            return query;
        }      

        // POST: api/Currencies
        [HttpPost]
        [ActionName("AddCurrency")]
        public async Task<ActionResult<Currencies>> AddCurrency(Currencies currencies)
        {
            _context.Currencies.Add(currencies);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCurrencies", new { id = currencies.Id }, currencies);
        }

        [HttpPost]
        [ActionName("UpdateCurrency")]
        public async Task<ActionResult<Currencies>> UpdateCurrency(Currencies currencies)
        {
            var task = _context.Currencies.FindAsync(currencies.Id);
            if(task.IsCompletedSuccessfully && task.Result != null)
            {
                var currency = task.Result;
                currency = currencies;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCurrencies", new { id = currency.Id }, currency);

            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/Currencies/5
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

        private bool CurrenciesExists(int id)
        {
            return _context.Currencies.Any(e => e.Id == id);
        }
    }
}
