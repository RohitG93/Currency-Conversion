using Currency_Conversion.Models;

namespace Currency_Conversion.Services
{
    public class CurrencyExchangesService : ICurrencyExchangesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyExchangesService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<GetExchangeRate> GetCurrencyAsync(string fromCurrency, string toCurrency)
        {
            // Read URL from environment variable
            var baseUrl =
                _configuration["Services:ExchangeService"];

            // Build complete URL
            var url = $"{baseUrl}/api/CurrencyExchange/GetExchangeRate/{fromCurrency}/{toCurrency}";

            var response = await _httpClient.GetFromJsonAsync<GetExchangeRate>(url);

            return response;
        }
    }
}
