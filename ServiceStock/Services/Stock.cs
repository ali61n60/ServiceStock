namespace ServiceStock.Services
{
    public class Stock
    {
        public string Symbol { get; set; }
        public double LastPrice { get; set; }
        public override string ToString()
        {
            return $"[Stock: Symbol={Symbol}, LastPrice={LastPrice}]";
        }
    }
}