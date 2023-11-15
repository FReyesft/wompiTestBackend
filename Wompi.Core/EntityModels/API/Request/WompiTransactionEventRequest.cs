using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wompi.Core.EntityModels.API.Request
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public Transaction transaction { get; set; }
    }

    public class WompiTransactionEventRequest
    {
        public string @event { get; set; }
        public Data data { get; set; }
        public string environment { get; set; }
        public Signature signature { get; set; }
        public int timestamp { get; set; }
        public DateTime sent_at { get; set; }
    }

    public class Signature
    {
        public List<string> properties { get; set; }
        public string checksum { get; set; }
    }

    public class Transaction
    {
        public string id { get; set; }
        public int amount_in_cents { get; set; }
        public string reference { get; set; }
        public string customer_email { get; set; }
        public string currency { get; set; }
        public string payment_method_type { get; set; }
        public string redirect_url { get; set; }
        public string status { get; set; }
        public object shipping_address { get; set; }
        public object payment_link_id { get; set; }
    }


}
