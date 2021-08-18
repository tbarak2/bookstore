using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //[JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("publish_date")]
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }
    }
}
