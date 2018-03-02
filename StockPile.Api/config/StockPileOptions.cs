namespace StockPile.Api.config
{
    /// <summary>
    /// Options class for injecting AppSettings into StockPile Api Controllers
    /// </summary>
    public class StockApiPileOptions
    {
        public StockApiPileOptions() { }

        public string ServiceBase { get; set; }
    }
}
