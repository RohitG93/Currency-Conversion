using Currency_Conversion.Models;

namespace Currency_Conversion.Services
{
    public interface ICurrencyExchangesService
    {
        public Task<GetExchangeRate> GetCurrencyAsync(string fromCurrency, string toCurrency);
    }
}
