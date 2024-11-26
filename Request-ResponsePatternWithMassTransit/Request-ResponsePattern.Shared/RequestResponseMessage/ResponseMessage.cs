using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request_ResponsePattern.Shared.RequestResponseMessage
{
    public record ResponseMessage
    {
        public string Text { get; set; }
    }
}
