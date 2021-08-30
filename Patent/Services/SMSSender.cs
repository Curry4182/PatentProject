using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patent.Services
{
    public class SMSSender
    {
        private readonly string phoneNumber;
        private readonly string apiKey;
        private readonly string apiSecret;
        private readonly string domain;
        private readonly string protocol;
        private readonly string prefix;
        public SMSSender(
            string phoneNumber,
            string apiKey,
            string apiSecret,
            string domain = "api.solapi.com",
            string protocol = "https",
            string prefix = ""
            )
        {
            this.phoneNumber = phoneNumber;
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
            this.domain = domain;
            this.protocol = protocol;
            this.prefix = prefix;
        }


    }
}
