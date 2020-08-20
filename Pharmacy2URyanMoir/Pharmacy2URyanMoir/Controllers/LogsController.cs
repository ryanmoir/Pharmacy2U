using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy2URyanMoir.Data;
using Pharmacy2URyanMoir.Models;

namespace Pharmacy2URyanMoir.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly DbSetContext _context;

        public LogsController()
        {
            _context = new DbSetContext();
        }

        // GET: api/Logs/AllLogs
        [HttpGet]
        [ActionName("AllLogs")]
        public async Task<ActionResult<IEnumerable<Logs>>> GetLogs()
        {
            return await _context.Logs.ToListAsync();
        }

        // GET: api/Logs/DateLogs/startRange=a/endRange=b
        [HttpGet("{startRange}/{endRange}")]
        [ActionName("DateLogs")]
        public async Task<ActionResult<IEnumerable<Logs>>> GetLogs(string startRange, string endRange)
        {
            startRange = startRange.Replace("startRange=", "");
            endRange = endRange.Replace("endRange=", "");

            if(!DateTime.TryParse(startRange, out var startDateTime) || !DateTime.TryParse(endRange, out var endDateTime))
            {
                return BadRequest();
            }

            var query =  await _context.Logs
                .Where(l => l.DateTime >= startDateTime && l.DateTime <= endDateTime).ToListAsync();

            if (query == null)
            {
                return NotFound();
            }

            return query;
        }

        // GET: api/Logs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Logs>> GetLogs(int id)
        {
            var logs = await _context.Logs.FindAsync(id);

            if (logs == null)
            {
                return NotFound();
            }

            return logs;
        }
    }
}
