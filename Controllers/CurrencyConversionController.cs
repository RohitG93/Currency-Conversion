using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Currency_Conversion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConversionController : ControllerBase
    {
        public readonly Services.ICurrencyExchangesService _currencyService;

        public CurrencyConversionController(Services.ICurrencyExchangesService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Conversion api running successfully version 1.6");
        }

        [HttpGet("GetConversionDetail/{fromCurrency}/{toCurrency}/{amount}")]
        public async Task<IActionResult> GetExchangeRate(string fromCurrency, string toCurrency, decimal amount)
        {
            try
            {
                var result = await _currencyService.GetCurrencyAsync(fromCurrency, toCurrency);

                var exchangeRate = result?.ExchangeRate;
                if (exchangeRate.HasValue)
                {
                    return Ok(new
                    {
                        FromCurrency = fromCurrency,
                        ToCurrency = toCurrency,
                        ExchangeRate = exchangeRate.Value,
                        Amount = amount,
                        ConvertedAmount = amount * (decimal)exchangeRate.Value
                    });
                }
                return NotFound("Exchange rate not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving exchange rate: {ex.Message}");

            }
        }
    }
}
