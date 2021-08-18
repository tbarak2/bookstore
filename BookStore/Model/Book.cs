using Newtonsoft.Json;
using System;

namespace BookStore.Model
{
    public class Book
    {
        private decimal price;
        [JsonProperty("@id")]
        public string Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price 
        {
            get { return price; } 
            set { price = Decimal.Round(value); } 
        }

        [JsonProperty("publish_date")]
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
    }
}
