using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wompi.Core.EntityModels.API.Request
{
    public class WompiLinkRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool single_use { get; set; }
        public string currency { get; set; }
        public int amount_in_cents { get; set; }
        public bool collect_shipping { get; set; }
        public string redirect_url { get; set; }
    }


}
