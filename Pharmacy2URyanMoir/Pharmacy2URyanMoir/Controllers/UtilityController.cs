using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy2URyanMoir.Data;

namespace Pharmacy2URyanMoir.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        [HttpDelete]
        [ActionName("Clear")]
        //https://localhost:44318/api/Utility/Clear
        public void ClearDb()
        {
            using (var context = new DbSetContext())
            {
                context.Currencies.RemoveRange(context.Currencies);
                context.ExchangeRates.RemoveRange(context.ExchangeRates);
                context.Logs.RemoveRange(context.Logs);
                context.SaveChanges();
            }
        }
    }
}
