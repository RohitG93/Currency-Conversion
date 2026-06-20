namespace Currency_Conversion.Models
{
    public class GetExchangeRate
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double ExchangeRate { get; set; } = 0;
    }
}
